using SAT.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.Core.Helpers;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using SAT.Core.DL.DAO.Cancelation;
using SAT.Core.DL.DAO.Pendings;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.Implements.SQL.Repository.Entities;
using SAT.Core.DL.DAO.Relations;
using SAT.ConsultaCFDI;

namespace SAT.CancelaCFD
{
    [ServiceBehavior(Namespace = "http://cancelacfd.sat.gob.mx", AddressFilterMode = AddressFilterMode.Any)]
    public class CancelaCFDBindingEmulation : ICancelaCFDBindingEmulation
    {
        CancelationDAO _cancelation;
        PendingsDAO _pendings;
        ReceptionDAO _reception;
        RelationsDAO _relations;
        public CancelaCFDBindingEmulation(CancelationDAO cancelation, PendingsDAO pendings, ReceptionDAO reception, RelationsDAO relations)
        {
            _cancelation = cancelation;
            _pendings = pendings;
            _reception = reception;
            _relations = relations;
        }

        public Acuse CancelaCFD(Cancelacion cancelacion)
        {
            string xml = string.Empty;
            Acuse acuse = new Acuse();
            acuse.RfcEmisor = cancelacion.RfcEmisor;
            acuse.Fecha = cancelacion.Fecha;
            acuse.Signature = cancelacion.Signature;

            X509Certificate2 certificate = new X509Certificate2(cancelacion.Signature.KeyInfo.X509Data.X509Certificate);
            if (certificate == null)
            {
                acuse.CodEstatus = "305";
                return acuse;
            }
            if (!Sign.IsValidIssuer(certificate, cancelacion.RfcEmisor))
            {
                acuse.CodEstatus = "203";
                return acuse;
            }
            try
            {
                xml = XmlMessageSerializer.SerializeDocumentToXml(cancelacion);
            }
            catch
            {
                acuse.CodEstatus = "301";
                return acuse;
            }
            if (!SignatureHelper.ValidateSignatureXml(xml))
            {
                acuse.CodEstatus = "302";
                return acuse;
            }
            DateTime cancelationDate = DateTime.Parse(string.Format("{0:s}", DateTime.UtcNow), CultureInfo.InvariantCulture);

            acuse.Fecha = cancelationDate;
            List<AcuseFolios> acuseFolios = new List<AcuseFolios>();
            foreach (var folio in cancelacion.Folios)
            {
                var query = _reception.GetDocumentByUUID(Guid.Parse(folio.UUID));
                ConsultaCFDI.Acuse data = ConsultaCFDIServiceEmulation.GetLastUpdatedDocument(query.RfcEmisor, query.RfcReceptor, query.Total, query.UUID.ToString());
                AcuseFolios acuseFolio = new AcuseFolios();
                acuseFolio.UUID = folio.UUID;
                if (data == null)
                    acuseFolio.EstatusUUID = "205";
                else if (data.Estado == "Cancelado")
                {
                    acuseFolio.EstatusUUID = "202";
                }
                else if (query.RfcEmisor != acuse.RfcEmisor)
                {
                    acuseFolio.EstatusUUID = "203";
                }
                else
                {
                    acuseFolio.EstatusUUID = "201";
                }
                acuseFolios.Add(acuseFolio);
                ///Logica de negocio
                
            }
            acuse.Folios = acuseFolios.ToArray();

            acuse.RfcEmisor = cancelacion.RfcEmisor;
            acuse.Signature = cancelacion.Signature;
            acuse.CodEstatus = null;
            CancelFolios(cancelacion.Folios);
            return acuse;
        }


        private void CancelFolios(CancelacionFolios[] folios)
        {
            foreach (var f in folios)
            {
                
                var query = _reception.GetDocumentByUUID(Guid.Parse(f.UUID));
                ConsultaCFDI.Acuse doc = ConsultaCFDIServiceEmulation.GetLastUpdatedDocument(query.RfcEmisor, query.RfcReceptor,query.Total, query.UUID.ToString());
                
                if (doc.Estado != "Cancelado")
                {
                    if (doc.EsCancelable == "Cancelable con aceptacion")
                    {
                        if (_pendings.GetPendingByUUID(Guid.Parse(f.UUID)) == null)
                        {
                            _cancelation.StartCancelDocument(Guid.Parse(f.UUID));
                            _pendings.SendPending(Guid.Parse(f.UUID));
                        }
                    }
                    else
                    {
                        _cancelation.CancelDocument(Guid.Parse(f.UUID));
                    }
                }
            }
        }

        private bool IsGenerericRFC(Document cfdi)
        {
            string generic_rfc = "XAXX010101000";
            return cfdi.RfcReceptor == generic_rfc;
        }
        private bool IsForeignRFC(Document cfdi)
        {
            string generic_rfc = "XEXX010101000";
            return cfdi.RfcReceptor == generic_rfc;
        }
        private bool IsMore5K(Document cfd)
        {
            return decimal.Parse(cfd.Total) > 5000;
        }
        private bool IsEgresosNomina(Document document)
        {
            return document.TipoComprobante == "E" || document.TipoComprobante == "N";
        }

        private bool IsCancelledByTime(Guid uuid)
        {
            var doc = _reception.GetDocumentByUUID(uuid);
            var minutes = (int)(GetCentralTime() - doc.FechaEmision).TotalMinutes;
            return minutes > 10;
        }


        private DateTime GetCentralTime()
        {
            var centralTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), centralTimeZone);
        }



    }
}

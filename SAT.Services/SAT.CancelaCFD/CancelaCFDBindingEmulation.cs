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
            if (cancelacion.Folios.Any(w => _reception.GetDocumentByUUID(w.UUID) == null))
            {
                acuse.Folios = new AcuseFolios[cancelacion.Folios.Length];
                var acusesFolios = new List<AcuseFolios>();
                foreach (var folio in cancelacion.Folios)
                {
                    if (_reception.GetDocumentByUUID(folio.UUID) == null)
                    {
                        acusesFolios.Add(new AcuseFolios()
                        {
                            EstatusUUID = "205",
                            UUID = folio.UUID.ToUpper()
                        });
                    }
                    else
                    {
                        acusesFolios.Add(new AcuseFolios()
                        {
                            EstatusUUID = "201",
                            UUID = folio.UUID.ToUpper()
                        });
                    }

                }
                acuse.Folios = acusesFolios.ToArray();

                acuse.RfcEmisor = cancelacion.RfcEmisor;
                acuse.Signature = cancelacion.Signature;
                acuse.CodEstatus = "205";
                return acuse;
            }

            if (cancelacion.Folios.Any(w => _pendings.GetPendingByUUID(w.UUID)!= null))
            {
                acuse.Folios = new AcuseFolios[cancelacion.Folios.Length];
                var acusesFolios = new List<AcuseFolios>();
                foreach (var folio in cancelacion.Folios)
                {
                    if (_pendings.GetPendingByUUID(folio.UUID) != null)
                    {
                        acusesFolios.Add(new AcuseFolios()
                        {
                            EstatusUUID = "1004",
                            UUID = folio.UUID.ToUpper()
                        });
                    }
                    else
                    {
                        acusesFolios.Add(new AcuseFolios()
                        {
                            EstatusUUID = "1000",
                            UUID = folio.UUID.ToUpper()
                        });
                    }

                }
                acuse.Folios = acusesFolios.ToArray();

                acuse.RfcEmisor = cancelacion.RfcEmisor;
                acuse.Signature = cancelacion.Signature;
                acuse.CodEstatus = "1004";
                return acuse;
            }

            if (cancelacion.Folios.Any(w => _relations.GetRelationsParents(w.UUID)?.Count() > 0))
            {
                acuse.Folios = new AcuseFolios[cancelacion.Folios.Length];
                var acusesFolios = new List<AcuseFolios>();
                foreach (var folio in cancelacion.Folios)
                {
                    if (_relations.GetRelationsParents(folio.UUID).Count() > 0)
                    {
                        acusesFolios.Add(new AcuseFolios()
                        {
                            EstatusUUID = "301",
                            UUID = folio.UUID.ToUpper()
                        });
                    }
                    else
                    {
                        acusesFolios.Add(new AcuseFolios()
                        {
                            EstatusUUID = "201",
                            UUID = folio.UUID.ToUpper()
                        });
                    }
                    
                }
                acuse.Folios = acusesFolios.ToArray();

                acuse.RfcEmisor = cancelacion.RfcEmisor;
                acuse.Signature = cancelacion.Signature;
                acuse.CodEstatus = "301";
                return acuse;
            }
            if (cancelacion.Folios?.Count() > 0)
            {
                acuse.Folios = new AcuseFolios[cancelacion.Folios.Length];
                var acusesFolios = new List<AcuseFolios>();
                foreach (var folio in cancelacion.Folios)
                {
                    acusesFolios.Add(new AcuseFolios()
                    {
                        EstatusUUID = "201",
                        UUID = folio.UUID.ToUpper()
                    });
                }
                acuse.Folios = acusesFolios.ToArray();
            }
            acuse.RfcEmisor = cancelacion.RfcEmisor;
            acuse.Signature = cancelacion.Signature;

            CancelFolios(cancelacion.Folios);
            return acuse;
        }

        private void CancelFolios(CancelacionFolios[] folios)
        {
            foreach (var f in folios)
            {
                var doc = _reception.GetDocumentByUUID(f.UUID);
                if (IsCancelledByTime(f.UUID) && !IsGenerericRFC(doc) && !IsForeignRFC(doc) && IsMore5K(doc) && !IsEgresosNomina(doc))
                {
                    if (_pendings.GetPendingByUUID(f.UUID) == null)
                    {
                        _cancelation.StartCancelDocument(f.UUID);
                        _pendings.SendPending(f.UUID);
                    }
                   
                }
                else
                {
                    _cancelation.CancelDocument(f.UUID);
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

        private bool IsCancelledByTime(string uuid)
        {
            var doc = _reception.GetDocumentByUUID(uuid);
            var minutes = (int)(GetCentralTime() - doc.FechaEmision).TotalMinutes;
            return minutes > 10;
        }


        private DateTime GetCentralTime()
        {
            TimeZoneInfo setTimeZoneInfo;
            setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);
        }



    }
}

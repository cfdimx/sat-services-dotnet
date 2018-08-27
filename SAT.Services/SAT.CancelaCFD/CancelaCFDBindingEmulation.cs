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

namespace SAT.CancelaCFD
{
    [ServiceBehavior(Namespace = "http://cancelacfd.sat.gob.mx", AddressFilterMode = AddressFilterMode.Any)]
    public class CancelaCFDBindingEmulation : ICancelaCFDBindingEmulation
    {
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

            if (cancelacion.Folios?.Count() > 0)
            {
                acuse.Folios = new AcuseFolios[cancelacion.Folios.Length];
                var acusesFolios = new List<AcuseFolios>();
                foreach (var folio in cancelacion.Folios)
                {
                    acusesFolios.Add(new AcuseFolios()
                    {
                        EstatusUUID = "201",
                        UUID = folio.UUID
                    });
                }
                acuse.Folios = acusesFolios.ToArray();
            }
            acuse.RfcEmisor = cancelacion.RfcEmisor;
            acuse.Signature = cancelacion.Signature;

            return acuse;
        }
    }
}

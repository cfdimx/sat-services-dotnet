using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SAT.Core.Helpers
{
    public class SignatureHelper
    {
        public static bool ValidateSignatureXml(string xml)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                SignedXml signedXml = new SignedXml(xmlDoc);
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
                XmlNodeList certificates = xmlDoc.GetElementsByTagName("X509Certificate");
                X509Certificate2 dcert2 = new X509Certificate2(Convert.FromBase64String(certificates[0].InnerText));
                foreach (XmlElement element in nodeList)
                {
                    signedXml.LoadXml(element);
                    if (!signedXml.CheckSignature(dcert2, true))
                        throw new Exception("Invalid Signature");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static (string taxId, string legalTaxId) CertificateGetInfoTaxId(byte[] certificate)
        {
            try
            {
                X509Certificate2 x509Certificate = new X509Certificate2(certificate);
                var taxId = CertificateTaxId(x509Certificate);
                var taxIdLegal = CertificateLegalTaxId(x509Certificate);
                return (taxId, taxIdLegal);
            }
            catch (Exception)
            {
                return ("", "");
            }
        }
        public static string CertificateTaxId(X509Certificate2 certificate)
        {
            string taxIdCertificate = "";
            string[] subjects = certificate.SubjectName.Name.Trim().Split(',');
            foreach (var strTemp in subjects.ToList())
            {
                string[] strConceptoTemp = strTemp.Split('=');
                if (strConceptoTemp[0].Trim() == "OID.2.5.4.45")
                {
                    taxIdCertificate = strConceptoTemp[1].Trim().Split('/')[0];
                    //Bug Fix replace "
                    taxIdCertificate = taxIdCertificate.Replace("\"", "");
                    break;
                }
            }
            return taxIdCertificate.Trim().ToUpper();
        }
        public static string CertificateLegalTaxId(X509Certificate2 certificate)
        {
            string taxIdCertificate = "";
            string[] subjects = certificate.SubjectName.Name.Trim().Split(',');
            foreach (var strTemp in subjects.ToList())
            {
                string[] strConceptoTemp = strTemp.Split('=');
                if (strConceptoTemp[0].Trim() == "OID.2.5.4.45")
                {
                    if (strConceptoTemp[1].Trim().Split('/').Length == 2)
                        taxIdCertificate = strConceptoTemp[1].Trim().Split('/')[1];

                    //Bug Fix replace "
                    taxIdCertificate = taxIdCertificate.Replace("\"", "");
                    break;
                }
            }
            return taxIdCertificate.Trim().ToUpper();
        }
    }
}

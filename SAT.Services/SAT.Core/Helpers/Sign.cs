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
    public static class Sign
    {
        public static bool IsValidIssuer(X509Certificate2 X509Certificate, string RfcReceptor)
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(X509Certificate);

                string taxIdCertificate = "";
                string[] subjects = certificate.SubjectName.Name.Trim().Split(',');
                foreach (var strTemp in subjects.ToList())
                {
                    string[] strConceptoTemp = strTemp.Split('=');
                    if (strConceptoTemp[0].Trim() == "OID.2.5.4.45")
                    {
                        taxIdCertificate = strConceptoTemp[1].Trim().Split('/')[0];

                        taxIdCertificate = taxIdCertificate.Replace("\"", "");
                        break;
                    }
                }
                return taxIdCertificate.Trim().ToUpper() == RfcReceptor.Trim().ToUpper();
            }
            catch { 
                return false;
            }
        }
        public static bool Verify(string xmlCancelacion)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(GetString(xmlCancelacion));
            SignedXml signedXml = new SignedXml(xmlDoc);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
            XmlNodeList certificates = xmlDoc.GetElementsByTagName("X509Certificate");
            X509Certificate2 dcert2 = new X509Certificate2(Convert.FromBase64String(certificates[0].InnerText));
            signedXml.LoadXml((XmlElement)nodeList[0]);

            foreach (XmlElement element in nodeList)
            {
                signedXml.LoadXml(element);
                bool passes = signedXml.CheckSignature(dcert2, true);
            }

            return false;
        }
        private static string GetString(string xml)
        {
           // xml = xml.Replace("\r\n", "");
            xml = xml.Replace("\r", "");
            xml = xml.Replace("\n", "");
            xml = xml.Replace("xmlns:xsd=\"http://www.sat.gob.mx/cfd/3\"", "");
            xml = xml.Replace(@"<?xml version=""1.0"" encoding=""utf-16""?>", @"<?xml version=""1.0"" encoding=""utf-8""?>").Trim();
            xml = xml.Replace("﻿", "");
           xml = xml.Replace(@"
//", "");
            return xml;
        }
    }
}

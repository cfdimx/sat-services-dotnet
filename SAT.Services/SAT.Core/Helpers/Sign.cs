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
        public static bool Verify(string xml, byte[] signature, X509Certificate2 X509Certificate)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(GetString(xml));
            xmlDoc.DocumentElement.RemoveChild(xmlDoc.DocumentElement["Signature"]);
            //XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
            //signedXml.LoadXml((XmlElement)nodeList[0]);
            //return signedXml.CheckSignature(X509Certificate);
            //using (RSA rsaKey = X509Certificate.GetRSAPrivateKey())
            //{
            //    if (xmlDoc == null)
            //        throw new ArgumentException("xml content");
            //    SignedXml signedXml = new SignedXml(xmlDoc);
            //    //XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
            //    //signedXml.LoadXml((XmlElement)nodeList[0]);
            //    return signedXml.CheckSignature(X509Certificate);
            //}
            X509Certificate2 cert = new X509Certificate2(X509Certificate);
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(xmlDoc.OuterXml);
            byte[] hash = new SHA1Managed().ComputeHash(data);
            var res = csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);

            return res;
        }
        private static string GetString(string xml)
        {
            xml = xml.Replace("\r\n", "");
            xml = xml.Replace("\r", "");
            xml = xml.Replace("\n", "");
            xml = xml.Replace("xmlns:xsd=\"http://www.sat.gob.mx/cfd/3\"", "");
            xml = xml.Replace(@"<?xml version=""1.0"" encoding=""utf-16""?>", @"<?xml version=""1.0"" encoding=""utf-8""?>").Trim();
            xml = xml.Replace("﻿", "");
            xml = xml.Replace(@"
", "");
            return xml;
        }
    }
}

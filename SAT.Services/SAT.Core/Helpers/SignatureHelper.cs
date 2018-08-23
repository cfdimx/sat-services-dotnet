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
    }
}

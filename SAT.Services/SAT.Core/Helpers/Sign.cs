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
    }
}

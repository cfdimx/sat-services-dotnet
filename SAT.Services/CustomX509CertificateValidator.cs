using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace SAT.Services
{
    public class CustomX509CertificateValidator : X509CertificateValidator
    {
        string allowedIssuerName;

        public CustomX509CertificateValidator(string allowedIssuerName)
        {
            this.allowedIssuerName = allowedIssuerName ?? throw new ArgumentNullException("allowedIssuerName");
        }

        public override void Validate(X509Certificate2 certificate)
        {
            // Check that there is a certificate.
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            // Check that the certificate issuer matches the configured issuer.
            if (allowedIssuerName != certificate.IssuerName.Name)
            {
                throw new Exception
                  ("Certificate was not issued by a trusted issuer");
            }
        }
    }
}
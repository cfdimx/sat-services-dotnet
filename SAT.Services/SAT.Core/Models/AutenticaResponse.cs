using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.Autenticacion.Models
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "AutenticaResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
    public partial class AutenticaResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
        public string AutenticaResult;

        public AutenticaResponse()
        {
        }

        public AutenticaResponse(string AutenticaResult)
        {
            this.AutenticaResult = AutenticaResult;
        }
    }
}

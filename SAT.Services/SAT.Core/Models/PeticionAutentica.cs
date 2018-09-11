/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ServiceModel;

namespace SAT.Autenticacion.Models
{

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Autentica", WrapperNamespace = "http://schemas.xmlsoap.org/soap/envelope/,", IsWrapped = true)]
    public partial class AutenticaRequest
    {
        [MessageHeader(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")] public object Security { get; set; }
        public AutenticaRequest()
        {
        }
    }
  

}

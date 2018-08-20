using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SAT.ConsultaCFDI
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Acuse", Namespace = "http://schemas.datacontract.org/2004/07/Sat.Cfdi.Negocio.ConsultaCfdi.Servicio")]
    [System.SerializableAttribute()]    
    public partial class Acuse
    {

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoEstatusField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EsCancelableField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EstadoField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EstatusCancelacionField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string CodigoEstatus
        {
            get
            {
                return this.CodigoEstatusField;
            }
            set
            {
                if ((object.ReferenceEquals(this.CodigoEstatusField, value) != true))
                {
                    this.CodigoEstatusField = value;                    
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string EsCancelable
        {
            get
            {
                return this.EsCancelableField;
            }
            set
            {
                if ((object.ReferenceEquals(this.EsCancelableField, value) != true))
                {
                    this.EsCancelableField = value;                    
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string Estado
        {
            get
            {
                return this.EstadoField;
            }
            set
            {
                if ((object.ReferenceEquals(this.EstadoField, value) != true))
                {
                    this.EstadoField = value;                    
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string EstatusCancelacion
        {
            get
            {
                return this.EstatusCancelacionField;
            }
            set
            {
                if ((object.ReferenceEquals(this.EstatusCancelacionField, value) != true))
                {
                    this.EstatusCancelacionField = value;
                }
            }
        }
    }
}
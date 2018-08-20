using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SAT.ConsultaCFDI
{
    [System.Runtime.Serialization.DataContractAttribute(Name = "Acuse", Namespace = "http://schemas.datacontract.org/2004/07/Sat.Cfdi.Negocio.ConsultaCfdi.Servicio")]
    public partial class Acuse
    {

        [DataMember]
        private string CodigoEstatusField;

        [DataMember]
        private string EsCancelableField;

        [DataMember]
        private string EstadoField;

        [DataMember]
        private string EstatusCancelacionField;

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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
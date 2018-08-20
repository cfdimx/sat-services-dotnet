using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.Core;

namespace SAT.CfdiConsultaRelacionados
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cancelacfd.sat.gob.mx")]
    public partial class PeticionConsultaRelacionados
    {

        private SignatureType signatureField;

        private string uuidField;

        private string rfcReceptorField;

        private string rfcPacEnviaSolicitudField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", Order = 0)]
        public SignatureType Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Uuid
        {
            get
            {
                return this.uuidField;
            }
            set
            {
                this.uuidField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RfcReceptor
        {
            get
            {
                return this.rfcReceptorField;
            }
            set
            {
                this.rfcReceptorField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RfcPacEnviaSolicitud
        {
            get
            {
                return this.rfcPacEnviaSolicitudField;
            }
            set
            {
                this.rfcPacEnviaSolicitudField = value;                
            }
        }
    }
}

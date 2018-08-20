using SAT.Core;

namespace SAT.CancelaCFD
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2117.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cancelacfd.sat.gob.mx")]
    public partial class Cancelacion
    {

        private CancelacionFolios[] foliosField;

        private SignatureType signatureField;

        private string rfcEmisorField;

        private System.DateTime fechaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Folios", Order = 0)]
        public CancelacionFolios[] Folios
        {
            get
            {
                return this.foliosField;
            }
            set
            {
                this.foliosField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", Order = 1)]
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
        public string RfcEmisor
        {
            get
            {
                return this.rfcEmisorField;
            }
            set
            {
                this.rfcEmisorField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime Fecha
        {
            get
            {
                return this.fechaField;
            }
            set
            {
                this.fechaField = value;                
            }
        }
    }
}
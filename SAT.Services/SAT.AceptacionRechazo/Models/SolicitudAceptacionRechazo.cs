using SAT.Core;
using System;


namespace SAT.AceptacionRechazo
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cancelacfd.sat.gob.mx")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://cancelacfd.sat.gob.mx")]
    public class SolicitudAceptacionRechazo
    {

        private SolicitudAceptacionRechazoFolios[] foliosField;

        private SignatureType signatureField;

        private string rfcReceptorField;

        private string rfcPacEnviaSolicitudField;

        private System.DateTime fechaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Folios", Order = 0)]
        public SolicitudAceptacionRechazoFolios[] Folios
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

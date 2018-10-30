namespace SAT.AceptacionRechazo
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://cancelacfd.sat.gob.mx")]
    public partial class SolicitudAceptacionRechazoFolios
    {

        private string uUIDField;

        private TipoAccionPeticionCancelacion respuestaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public TipoAccionPeticionCancelacion Respuesta
        {
            get
            {
                return this.respuestaField;
            }
            set
            {
                this.respuestaField = value;                
            }
        }
    }
}

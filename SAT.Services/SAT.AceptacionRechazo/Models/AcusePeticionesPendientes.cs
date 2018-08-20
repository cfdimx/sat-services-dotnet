namespace SAT.AceptacionRechazo
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cancelacfd.sat.gob.mx")]
    public partial class AcusePeticionesPendientes
    {

        private string[] uUIDField;

        private string codEstatusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UUID", Order = 0)]
        public string[] UUID
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CodEstatus
        {
            get
            {
                return this.codEstatusField;
            }
            set
            {
                this.codEstatusField = value;                
            }
        }
    }
}

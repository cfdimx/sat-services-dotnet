namespace SAT.CancelaCFD
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2117.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://cancelacfd.sat.gob.mx")]
    public partial class AcuseFolios
    {

        private string uUIDField;

        private string estatusUUIDField;

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
        public string EstatusUUID
        {
            get
            {
                return this.estatusUUIDField;
            }
            set
            {
                this.estatusUUIDField = value;
            }
        }
    }
}
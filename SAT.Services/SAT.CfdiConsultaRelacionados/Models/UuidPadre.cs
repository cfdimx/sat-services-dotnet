using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CfdiConsultaRelacionados
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cancelacfd.sat.gob.mx")]
    public partial class UuidPadre : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string uuidField;

        private string rfcEmisorField;

        private string rfcReceptorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Uuid
        {
            get
            {
                return this.uuidField;
            }
            set
            {
                this.uuidField = value;
                this.RaisePropertyChanged("Uuid");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string RfcEmisor
        {
            get
            {
                return this.rfcEmisorField;
            }
            set
            {
                this.rfcEmisorField = value;
                this.RaisePropertyChanged("RfcEmisor");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string RfcReceptor
        {
            get
            {
                return this.rfcReceptorField;
            }
            set
            {
                this.rfcReceptorField = value;
                this.RaisePropertyChanged("RfcReceptor");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

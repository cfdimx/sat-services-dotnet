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
    public partial class ConsultaRelacionados
    {

        private string uuidConsultadoField;

        private string resultadoField;

        private UuidPadre[] uuidsRelacionadosPadresField;

        private UuidRelacionado[] uuidsRelacionadosHijosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string UuidConsultado
        {
            get
            {
                return this.uuidConsultadoField;
            }
            set
            {
                this.uuidConsultadoField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Resultado
        {
            get
            {
                return this.resultadoField;
            }
            set
            {
                this.resultadoField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
        public UuidPadre[] UuidsRelacionadosPadres
        {
            get
            {
                return this.uuidsRelacionadosPadresField;
            }
            set
            {
                this.uuidsRelacionadosPadresField = value;                
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
        public UuidRelacionado[] UuidsRelacionadosHijos
        {
            get
            {
                return this.uuidsRelacionadosHijosField;
            }
            set
            {
                this.uuidsRelacionadosHijosField = value;         
            }
        }
    }
}

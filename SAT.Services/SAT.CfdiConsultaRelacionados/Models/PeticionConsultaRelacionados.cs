using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SAT.Core;

namespace SAT.CfdiConsultaRelacionados
{
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cancelacfd.sat.gob.mx")]
    [DataContract(Namespace = "http://cancelacfd.sat.gob.mx")]
    public partial class PeticionConsultaRelacionados
    {
        /// <remarks/>

        [XmlElement(Order = 0)]
        [DataMember]
        public SignatureType Signature { get; set; }
        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public string Uuid { get; set; }

        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public string RfcReceptor { get; set; }
        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public string RfcPacEnviaSolicitud { get; set; }
    }
}

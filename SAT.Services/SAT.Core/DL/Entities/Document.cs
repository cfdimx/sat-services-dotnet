using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.Entities
{
    public class Document
    {
        public string XmlUrl { get; set; }
        public string CodigoEstatus { get; set; }
        public string EsCancelable { get; set; }
        public string Estado { get; set; }
        public string EstatusCancelacion { get; set; }
        public string UUID { get; set; }
    }
}

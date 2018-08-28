using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.Entities
{
    public class DocumentBase : IDocument
    {
        [Key]
        public int id { get; set; }
        public string XmlUrl { get; set; }
        public string CodigoEstatus { get; set; }
        public string EsCancelable { get; set; }
        public string Estado { get; set; }
        public string EstatusCancelacion { get; set; }
        public string UUID { get; set; }
    }
}

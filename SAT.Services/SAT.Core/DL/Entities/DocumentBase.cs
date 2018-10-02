using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.Entities
{
    public class DocumentBase : IDocument, IEntity
    {
        [Key]
        public Guid UUID { get; set; }
        public string XmlUrl { get; set; }
        public string CodigoEstatus { get; set; }
        public string EsCancelable { get; set; }
        public string Estado { get; set; }
        public string EstatusCancelacion { get; set; }
        
        public string TipoComprobante { get; set; }
        public string Total { get; set; }
        public string RfcReceptor { get; set; }
        public string RfcEmisor { get; set; }
        public string VersionComprobante { get; set; }
        public string NumeroCertificado { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaTimbrado { get; set; }
        
    }
}

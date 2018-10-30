using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.Entities
{
    public class PendingCancelation: IEntity
    {
        [Key]
        public Guid UUID { get; set; }
        public string RfcReceptor { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }
}

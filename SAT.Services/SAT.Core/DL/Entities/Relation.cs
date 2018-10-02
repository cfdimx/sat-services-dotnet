using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.Entities
{
    public class Relation: IEntity
    {
        [Key]
        public double id { get; set; }
        public Guid UUID { get; set; }
        public Guid ParentUUID { get; set; }
        public string RelationType { get; set; }
    }
}

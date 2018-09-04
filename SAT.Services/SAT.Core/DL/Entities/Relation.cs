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
        public long id { get; set; }
        public string UUID { get; set; }
        public string ParentUUID { get; set; }
        public string RelationType { get; set; }
    }
}

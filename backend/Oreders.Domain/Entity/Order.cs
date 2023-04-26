using Oreders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime? Created { get; set; }
        public bool IsDeleted { get; set; }
        public List<Relation> Relations { get; set; }
    }
}

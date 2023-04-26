using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Entity
{
    public class Relation
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public uint Count { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}

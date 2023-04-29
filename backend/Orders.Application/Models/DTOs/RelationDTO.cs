using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Models.DTOs
{
    public class RelationDTO
    {
        public uint Count { get; set; }
        public Guid ProductId { get; set; }
    }
}

using Oreders.Domain.Entity;
using Oreders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Models.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public List<RelationDTO> Relations { get; set; }
    }
}

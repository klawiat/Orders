using Oreders.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public Guid id { get; set; }
        [Required]
        public List<RelationViewModel> lines { get; set; }
    }
}

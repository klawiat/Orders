using Oreders.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebApi.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string Name { get; set; }
        //public bool IsDeleted { get; set; }
    }
}

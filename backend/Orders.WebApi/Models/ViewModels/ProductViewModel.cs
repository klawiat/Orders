using Oreders.Domain.Entity;

namespace Orders.WebApi.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}

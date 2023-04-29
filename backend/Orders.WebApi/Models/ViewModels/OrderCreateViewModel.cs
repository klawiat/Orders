using Oreders.Domain.Enums;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public Guid id { get; set; }
        public List<RelationViewModel> lines { get; set; }
    }
}

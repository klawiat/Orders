using Oreders.Domain.Enums;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public Guid Id { get; set; }
        public List<RelationViewModel> Lines { get; set; }
    }
}

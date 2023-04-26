using Oreders.Domain.Enums;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderEditViewModel
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public List<RelationViewModel> Lines { get; set; }
    }
}

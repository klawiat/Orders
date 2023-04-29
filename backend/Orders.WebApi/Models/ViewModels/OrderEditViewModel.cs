using Newtonsoft.Json;
using Oreders.Domain.Enums;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderEditViewModel
    {
        [JsonIgnore]
        public Guid id { get; set; }
        public string status { get; set; }
        public List<RelationViewModel> lines { get; set; }
    }
}

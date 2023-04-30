using System.Text.Json.Serialization;

namespace Orders.WebApi.Models.ViewModels
{
    public class EditOrderViewModel
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("lines")]
        public List<OrderItemViewModel> Items { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Orders.WebApi.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("lines")]
        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}

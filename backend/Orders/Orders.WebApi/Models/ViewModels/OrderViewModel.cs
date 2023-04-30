using System.Text.Json.Serialization;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderViewModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("lines")]
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}

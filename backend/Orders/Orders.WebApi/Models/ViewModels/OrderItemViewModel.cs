using System.Text.Json.Serialization;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderItemViewModel
    {
        [JsonPropertyName("id")]
        public string ProductId { get; set; }
        [JsonPropertyName("qty")]
        public int Quantity { get; set; }
    }
}

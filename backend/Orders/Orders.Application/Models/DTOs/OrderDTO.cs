using Orders.Domain.Enums;

namespace Orders.Application.Models.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}

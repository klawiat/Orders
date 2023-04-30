using Orders.Domain.Enums;

namespace Orders.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public List<OrderItem> Lines { get; set; } = new List<OrderItem>();
    }
}

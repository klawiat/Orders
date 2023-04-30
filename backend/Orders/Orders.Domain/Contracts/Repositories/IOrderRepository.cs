using Orders.Domain.Entities;

namespace Orders.Domain.Contracts.Repositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetAllAsync();
        public Task<Order?> GetAsync(Guid id);
        public Task<Order?> GetWithoutProductsAsync(Guid id);
        public Task<Order?> CreateAsync(Order order);
        public Task<Order?> UpdateAsync(Order order);
        public Task DeleteAsync(Guid id);
    }
}

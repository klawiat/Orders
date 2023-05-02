using Microsoft.EntityFrameworkCore;
using Orders.Domain.Contracts.Repositories;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.Implementations.Repository
{
    public class OrderRepository : IOrderRepository
    {
        #region Свойства
        private readonly OrdersDBContext context;
        private readonly DbSet<Order> orders;
        #endregion
        public OrderRepository(OrdersDBContext context)
        {
            this.context = context;
            this.orders = context.orders;
            this.orders = context.orders;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            await orders.AddAsync(order);
            await context.products.AddRangeAsync(order.Lines);
            await context.SaveChangesAsync();
            return order;
        }
        /// <summary>
        /// Удаляет заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Ничего</returns>
        public async Task DeleteAsync(Guid id)
        {
            Order? order = await orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            orders.Remove(order);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Получить список всех заказов
        /// </summary>
        /// <returns>Список найденных заказов</returns>
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await orders.Include(order => order.Lines).AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// Получить заказ по идентификатору с подгрузкой дополнительных данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденный заказ</returns>
        public async Task<Order?> GetAsync(Guid id)
        {
            return await orders.Include(order => order.Lines).AsNoTracking().FirstOrDefaultAsync(order => order.Id == id);
        }
        /// <summary>
        /// Получить заказ по идентификатору без подгрузки связей
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Найденный заказ</returns>
        public async Task<Order?> GetWithoutProductsAsync(Guid id)
        {
            return await orders.AsNoTracking().FirstOrDefaultAsync(order => order.Id == id);
        }
        /// <summary>
        /// Одновить заказ в бд
        /// </summary>
        /// <param name="order">Обновляемый заказ</param>
        /// <returns>Обновленный заказ</returns>
        public async Task<Order?> UpdateAsync(Order order)
        {
            List<OrderItem> items = await context.products.Where(item => item.OrderId == order.Id).AsNoTracking().ToListAsync();
            context.products.RemoveRange(items);
            orders.Update(order);
            context.products.AddRange(order.Lines);
            await context.SaveChangesAsync();
            return order;
        }
    }
}

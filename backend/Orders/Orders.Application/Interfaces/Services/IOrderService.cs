using Orders.Application.Models.DTOs;

namespace Orders.Application.Interfaces.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns>Все заказы из бд</returns>
        public Task<IEnumerable<OrderDTO>> GetOrders();
        /// <summary>
        /// Получить заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор нужного заказа</param>
        /// <returns>Заказ. В случае отсутствия такогого в бд выбросит исключение</returns>
        public Task<OrderDTO> GetOrderById(Guid id);
        /// <summary>
        /// Создать заказ
        /// </summary>
        /// <param name="orderDTO">Заказ который необходимо добавить в бд</param>
        /// <returns>Изменённый базой заказ</returns>
        public Task<OrderDTO> CreateOrder(OrderDTO orderDTO);
        /// <summary>
        /// Обновить заказ в бд
        /// </summary>
        /// <param name="orderDTO">Заказ который нужно обновить</param>
        /// <returns>Обновленный заказ</returns>
        public Task<OrderDTO> UpdateOrder(OrderDTO orderDTO);
        /// <summary>
        /// Удаляет заказ по идентификаторы
        /// </summary>
        /// <param name="id">Идентификатор нужного заказа</param>
        /// <returns>Результат асинхронного метода</returns>
        public Task DeleteOrder(Guid id);
    }
}

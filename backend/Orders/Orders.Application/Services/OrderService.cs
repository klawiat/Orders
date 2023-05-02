using AutoMapper;
using FluentValidation;
using Orders.Application.Exceptions;
using Orders.Application.Interfaces.Services;
using Orders.Application.Models.DTOs;
using Orders.Domain.Contracts.Repositories;
using Orders.Domain.Entities;
using Orders.Domain.Enums;

namespace Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        #region Свойства
        private readonly IOrderRepository orders;
        private readonly IMapper mapper;
        private readonly IValidator<OrderDTO> validator;
        #endregion
        public OrderService(IOrderRepository orders, IMapper mapper, IValidator<OrderDTO> validator)
        {
            this.orders = orders;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO orderDTO)
        {
            validator.ValidateAndThrow(orderDTO);
            if (await orders.GetWithoutProductsAsync(orderDTO.Id) is not null) throw new ConflictException("Заказ", $"с идентификатором {orderDTO.Id} уже существует");
            Order order = mapper.Map<Order>(orderDTO);
            await orders.CreateAsync(order);
            return mapper.Map<OrderDTO>(order);
        }

        public async Task DeleteOrder(Guid id)
        {
            Order order = await orders.GetWithoutProductsAsync(id);
            if (order is null) throw new NoDataException($"Заказ с идентификатором {id} не найден");
            if ((int)order.Status > (int)Status.Paid) throw new InvalidOperationException($"Заказ в статусе {order.Status} нельзя удалить");
            await orders.DeleteAsync(id);
        }

        public async Task<OrderDTO> GetOrderById(Guid id)
        {
            Order order = await orders.GetAsync(id);
            if (order is null) throw new NoDataException($"Заказ с идентификатором {id.ToString()} не найден");
            return mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            IEnumerable<Order> orderList = await orders.GetAllAsync();
            if (orderList is null || !orderList.Any()) throw new NoDataException("Заказы не найдены");
            return mapper.Map<IEnumerable<OrderDTO>>(orderList);
        }

        public async Task<OrderDTO> UpdateOrder(OrderDTO orderDTO)
        {
            validator.ValidateAndThrow(orderDTO);
            Order order = await orders.GetWithoutProductsAsync(orderDTO.Id);
            if (order is null)
                throw new NoDataException($"Заказ с идентификатором {orderDTO.Id} не найден");
            if ((int)order.Status > (int)Status.AwaitingPayment)
                throw new InvalidOperationException($"Невозможно изменить заказ в статусе {order.Status}");
            order = mapper.Map<Order>(orderDTO);
            order = await orders.UpdateAsync(order);
            return mapper.Map<OrderDTO>(order);
        }
    }
}

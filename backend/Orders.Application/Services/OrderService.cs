using AutoMapper;
using Orders.Application.Models;
using Orders.Application.Models.DTOs;
using Oreders.Domain.Entity;
using Oreders.Domain.Enums;
using Oreders.Domain.Extensions;
using Oreders.Domain.Interfaces;
using Oreders.Domain.Interfaces.Services;
using Oredrs.Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public class OrderService : IUnivercalService<OrderDTO>
    {
        readonly IRepository<Order> orders;
        readonly IRepository<Product> products;
        readonly IMapper mapper;
        public OrderService(IRepository<Order> orders,IRepository<Product> products,IMapper mapper)
        {
            this.orders = orders;
            this.products = products;
            this.mapper = mapper;
        }
        public async Task<IResponce<OrderDTO>> CreateAsync(OrderDTO order)
        {
            try
            {
                if (order.Relations is null && !order.Relations.Any())
                    throw new ArgumentNullException("Заказ не может быть пустым");
                if (order.Relations.Any(x => x.Count < 1))
                    throw new InvalidDataException("Количество не может быть меньше 1");
                Order newOrder = mapper.Map<Order>(order);
                await orders.Create(newOrder);
                order=mapper.Map<OrderDTO>(newOrder);
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.OK,Data=order };
            }
            catch (Exception ex)
            {
                return new BaseResponce<OrderDTO> { StatusCode= HttpStatusCode.BadRequest,Description=ex.Message };
            }
        }

        public async Task<IResponce<bool>> DeleteAsync(Guid id)
        {
            try
            {
                Order order = await orders.GetById(id);
                if ((int)order.Status >= (int)Status.SubmittedForDelivery)
                    return new BaseResponce<bool> { StatusCode = HttpStatusCode.NotModified, Description = $"Нельзя редактировать заказ в статусе {order.Status.GetDisplayName}" };
                await orders.Delete(id);
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.OK,Data=true};
            }
            catch (ArgumentNullException)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.NotFound, Data=false, Description = "Заказ не найден" };
            }
            catch (Exception ex)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.BadRequest,Data=false, Description = "Некорректные данные" };
            }
        }

        public async Task<IResponce<IEnumerable<OrderDTO>>> GetAllAsync()
        {
            try
            {
                IEnumerable<Order> orderList = await orders.GetAll();
                var orderDTOs = mapper.Map<List<OrderDTO>>(orderList);
                return new BaseResponce<IEnumerable<OrderDTO>> { StatusCode = HttpStatusCode.OK, Data = orderDTOs };
            }
            catch (ArgumentNullException)
            {
                return new BaseResponce<IEnumerable<OrderDTO>> { StatusCode = HttpStatusCode.NotFound, Description = "Нет заказов" };
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<OrderDTO>> { StatusCode = HttpStatusCode.InternalServerError, Description = "Произошла непредвиденная ошибка"};
            }
        }

        public async Task<IResponce<OrderDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                Order order = await orders.GetById(id);
                if (order is null)
                    return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.NotFound, Description = "Заказ не найден" };
                OrderDTO orderDTO = mapper.Map<OrderDTO>(order);
                return new BaseResponce<OrderDTO> {  StatusCode=HttpStatusCode.OK, Data= orderDTO };
            }
            catch (ArgumentNullException)
            {
                return new BaseResponce<OrderDTO> { StatusCode=HttpStatusCode.NotFound, Description="Заказ не найден" };
            }
            catch (Exception)
            {
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.BadRequest, Description = "Произошла непредвиденная ошибка" };
            }
        }

        public async Task<IResponce<OrderDTO>> UpdateAsync(OrderDTO order)
        {
            try
            {
                Order newOrder = mapper.Map<Order>(order);
                Order oldOrder = await orders.GetById(newOrder.Id);
                if (oldOrder is null)
                    return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.NotFound, Description = "Заказ не найден" };
                if ((int)oldOrder.Status>=(int)Status.Paid)
                    return new BaseResponce<OrderDTO> { StatusCode=HttpStatusCode.NotModified, Description =$"Нельзя редактировать заказ в статусе {oldOrder.Status.GetDisplayName()}"};
                if (newOrder.Relations.Any(x => x.Count < 1))
                    return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.BadRequest, Description = "Нельзя добавить меньше 1 товара" };
                foreach(var rel in newOrder.Relations)
                {
                    Product product = await products.GetById(rel.ProductId);
                    if (product is null)
                        return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.NotFound, Description = "Товар не найден" };
                }
                await orders.Update(newOrder);
                order = mapper.Map<OrderDTO>(newOrder);
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.OK, Data = order };
            }
            catch (Exception ex)
            {
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.BadRequest, Description = "Произошла непредвиденная ошибка" };
            }
        }
    }
}

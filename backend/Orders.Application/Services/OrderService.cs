using AutoMapper;
using Orders.Application.Models.DTOs;
using Oreders.Domain.Entity;
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
    public class OrderService : IOrderService<OrderDTO>
    {
        readonly IRepository<Order> orders;
        readonly IMapper mapper;
        public OrderService(IRepository<Order> orders,IMapper mapper)
        {
            this.orders = orders;
            this.mapper = mapper;
        }
        public async Task<IResponce<OrderDTO>> CreateAsync(OrderDTO order)
        {
            try
            {
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
                await orders.Delete(id);
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.OK,Data=true};
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.NotFound, Data=false, Description = ex.Message };
            }
            catch (InvalidOperationException ex)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.NotModified, Data = false, Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.BadGateway,Data=false, Description = ex.Message };
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
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<IEnumerable<OrderDTO>> { StatusCode = HttpStatusCode.NotFound, Description = "Заказы не найдены" };
            }
            catch (Exception ex)
            {
                return new BaseResponce<IEnumerable<OrderDTO>> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }

        public async Task<IResponce<OrderDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                Order order = await orders.GetById(id);
                OrderDTO orderDTO = mapper.Map<OrderDTO>(order);
                return new BaseResponce<OrderDTO> {  StatusCode=HttpStatusCode.OK, Data= orderDTO };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<OrderDTO> { StatusCode=HttpStatusCode.NotFound, Description=ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }

        public async Task<IResponce<OrderDTO>> UpdateAsync(OrderDTO order)
        {
            try
            {
                Order updatedOrder = mapper.Map<Order>(order);
                await orders.Update(updatedOrder);
                order = mapper.Map<OrderDTO>(updatedOrder);
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.OK, Data = order };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.NotFound, Description = ex.Message };
            }
            catch (InvalidOperationException ex)
            {
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.NotModified, Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<OrderDTO> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }
    }
}

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
    public class OrderService : IOrderService
    {
        readonly IRepository<Order> orders;
        public OrderService(IRepository<Order> orders)
        {
            this.orders = orders;
        }
        public async Task<IResponce<Order>> CreateAsync(Order order)
        {
            try
            {
                await orders.Create(order);
                return new BaseResponce<Order> { StatusCode = HttpStatusCode.OK,Data=order };
            }
            catch (Exception ex)
            {
                return new BaseResponce<Order> { StatusCode= HttpStatusCode.BadRequest,Description=ex.Message };
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

        public async Task<IResponce<IEnumerable<Order>>> GetAllAsync()
        {
            try
            {
                IEnumerable<Order> orderList = await orders.GetAll();
                return new BaseResponce<IEnumerable<Order>> { StatusCode = HttpStatusCode.OK, Data = orderList };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<IEnumerable<Order>> { StatusCode = HttpStatusCode.NotFound, Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<IEnumerable<Order>> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }

        public async Task<IResponce<Order>> GetByIdAsync(Guid id)
        {
            try
            {
                Order order = await orders.GetById(id);
                return new BaseResponce<Order> {  StatusCode=HttpStatusCode.OK, Data=order };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<Order> { StatusCode=HttpStatusCode.NotFound, Description=ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<Order> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }

        public async Task<IResponce<Order>> UpdateAsync(Order order)
        {
            try
            {
                await orders.Update(order);
                return new BaseResponce<Order> { StatusCode = HttpStatusCode.OK, Data = order };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<Order> { StatusCode = HttpStatusCode.NotFound, Description = ex.Message };
            }
            catch (InvalidOperationException ex)
            {
                return new BaseResponce<Order> { StatusCode = HttpStatusCode.NotModified, Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<Order> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }
    }
}

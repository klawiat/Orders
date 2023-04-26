using Oreders.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IResponce<Order>> GetByIdAsync(Guid id);
        Task<IResponce<IEnumerable<Order>>> GetAllAsync();
        Task<IResponce<Order>> CreateAsync(Order order);
        Task<IResponce<Order>> UpdateAsync(Order order);
        Task<IResponce<bool>> DeleteAsync(Guid id);
    }
}

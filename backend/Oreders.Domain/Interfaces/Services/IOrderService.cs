using Oreders.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Interfaces.Services
{
    public interface IOrderService<T>
    {
        Task<IResponce<T>> GetByIdAsync(Guid id);
        Task<IResponce<IEnumerable<T>>> GetAllAsync();
        Task<IResponce<T>> CreateAsync(T order);
        Task<IResponce<T>> UpdateAsync(T order);
        Task<IResponce<bool>> DeleteAsync(Guid id);
    }
}

using Oreders.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<IResponce<Product>> GetByIdAsync(Guid id);
        Task<IResponce<IEnumerable<Product>>> GetAllAsync();
        Task<IResponce<Product>> CreateAsync(Product product);
        Task<IResponce<Product>> UpdateAsync(Product product);
        Task<IResponce<bool>> DeleteAsync(Guid id);
    }
}

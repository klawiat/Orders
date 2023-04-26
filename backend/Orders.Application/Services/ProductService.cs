using Oreders.Domain.Entity;
using Oreders.Domain.Interfaces;
using Oreders.Domain.Interfaces.Services;
using Oredrs.Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> products;
        public ProductService(IRepository<Product> products)
        {
            this.products = products;
        }

        public async Task<IResponce<Product>> CreateAsync(Product product)
        {
            try
            {
                await products.Create(product);
            }
            catch (Exception ex)
            {
                return new BaseResponce<Product> { StatusCode=HttpStatusCode.BadGateway,Description=ex.Message };
            }
            return new BaseResponce<Product> { StatusCode = HttpStatusCode.OK,Data=product };
        }

        public async Task<IResponce<bool>> DeleteAsync(Guid id)
        {
            try
            {
                await products.Delete(id);
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.OK, Data = true, Description = "Успех" };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.NotFound, Data = false, Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.BadGateway, Description = ex.Message };
            }
        }

        public async Task<IResponce<IEnumerable<Product>>> GetAllAsync()
        {
            try
            {
                var productList = await products.GetAll();
                return new BaseResponce<IEnumerable<Product>> {  StatusCode = HttpStatusCode.OK,Data = productList };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<IEnumerable<Product>> { StatusCode = HttpStatusCode.NotFound, Data = new List<Product>(), Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<IEnumerable<Product>> { StatusCode = HttpStatusCode.BadGateway, Data = new List<Product>(), Description = ex.Message };
            }
        }

        public async Task<IResponce<Product>> GetByIdAsync(Guid id)
        {
            try
            {
                Product product = await products.GetById(id);
                return new BaseResponce<Product> {  StatusCode = HttpStatusCode.OK, Data = product };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<Product> { StatusCode = HttpStatusCode.NotFound, Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<Product> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }

        public async Task<IResponce<Product>> UpdateAsync(Product product)
        {
            try
            {
                await products.Update(product); 
                return new BaseResponce<Product> { StatusCode = HttpStatusCode.OK,Data = product };
            }
            catch (Exception ex)
            {
                return new BaseResponce<Product> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }
    }
}

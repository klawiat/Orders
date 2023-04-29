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
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public class ProductService : IProductService<ProductDTO>
    {
        private readonly IRepository<Product> products;
        readonly IMapper mapper;
        public ProductService(IRepository<Product> products,IMapper mapper)
        {
            this.products = products;
            this.mapper = mapper;
        }

        public async Task<IResponce<ProductDTO>> CreateAsync(ProductDTO product)
        {
            try
            {
                Product newProduct = mapper.Map<Product>(product);
                await products.Create(newProduct);
                product = mapper.Map<ProductDTO>(newProduct);
            }
            catch (Exception ex)
            {
                return new BaseResponce<ProductDTO> { StatusCode=HttpStatusCode.BadGateway,Description=ex.Message };
            }
            return new BaseResponce<ProductDTO> { StatusCode = HttpStatusCode.OK,Data=product };
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

        public async Task<IResponce<IEnumerable<ProductDTO>>> GetAllAsync()
        {
            try
            {
                var productList = await products.GetAll();
                var dtos = mapper.Map<List<ProductDTO>>(productList);
                return new BaseResponce<IEnumerable<ProductDTO>> {  StatusCode = HttpStatusCode.OK,Data = dtos };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<IEnumerable<ProductDTO>> { StatusCode = HttpStatusCode.NotFound, Data = new List<ProductDTO>(), Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<IEnumerable<ProductDTO>> { StatusCode = HttpStatusCode.BadGateway, Data = new List<ProductDTO>(), Description = ex.Message };
            }
        }

        public async Task<IResponce<ProductDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                Product product = await products.GetById(id);
                ProductDTO productDTO = mapper.Map<ProductDTO>(product);
                return new BaseResponce<ProductDTO> {  StatusCode = HttpStatusCode.OK, Data = productDTO };
            }
            catch (ArgumentNullException ex)
            {
                return new BaseResponce<ProductDTO> { StatusCode = HttpStatusCode.NotFound, Description = ex.Message };
            }
            catch (Exception ex)
            {
                return new BaseResponce<ProductDTO> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }

        public async Task<IResponce<ProductDTO>> UpdateAsync(ProductDTO product)
        {
            try
            {
                Product updatedProduct = mapper.Map<Product>(product);
                await products.Update(updatedProduct); 
                product=mapper.Map<ProductDTO>(updatedProduct);
                return new BaseResponce<ProductDTO> { StatusCode = HttpStatusCode.OK,Data = product };
            }
            catch (Exception ex)
            {
                return new BaseResponce<ProductDTO> { StatusCode = HttpStatusCode.BadRequest, Description = ex.Message };
            }
        }
    }
}

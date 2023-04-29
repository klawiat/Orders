using AutoMapper;
using Orders.Application.Models;
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
    public class ProductService : IUnivercalService<ProductDTO>
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
                Product product = await products.GetById(id);
                if (product is null)
                    return new BaseResponce<bool> { StatusCode = HttpStatusCode.NotFound, Description = "Продукт не найден" };
                await products.Delete(id);
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.OK, Data = true, Description = "Успех" };
            }
            catch (Exception)
            {
                return new BaseResponce<bool> { StatusCode = HttpStatusCode.BadGateway, Description = "Произошла непредвиденная ошибка" };
            }
        }

        public async Task<IResponce<IEnumerable<ProductDTO>>> GetAllAsync()
        {
            try
            {
                var productList = await products.GetAll();
                if (productList is null || !productList.Any())
                    return new BaseResponce<IEnumerable<ProductDTO>> { StatusCode = HttpStatusCode.NotFound, Description = "Продуктов нет" };
                var dtos = mapper.Map<List<ProductDTO>>(productList);
                return new BaseResponce<IEnumerable<ProductDTO>> {  StatusCode = HttpStatusCode.OK,Data = dtos };
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<ProductDTO>> { StatusCode = HttpStatusCode.BadGateway, Data = new List<ProductDTO>(), Description = "Произошла непредвиденная ошибка" };
            }
        }

        public async Task<IResponce<ProductDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                Product product = await products.GetById(id);
                if (product is null)
                    return new BaseResponce<ProductDTO> { StatusCode = HttpStatusCode.NotFound, Description = "Продукт не найден" };
                ProductDTO productDTO = mapper.Map<ProductDTO>(product);
                return new BaseResponce<ProductDTO> {  StatusCode = HttpStatusCode.OK, Data = productDTO };
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
                Product oldProduct = await products.GetById(product.Id);
                if (oldProduct is null)
                    return new BaseResponce<ProductDTO> { StatusCode = HttpStatusCode.NotFound, Description = "Продукт не найден" };
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

using Microsoft.EntityFrameworkCore;
using Oreders.Domain.Entity;
using Oreders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredrs.Infrastructure.Implementations.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        readonly OrdersDbContext context;
        readonly DbSet<Product> products;
        public ProductRepository(OrdersDbContext context)
        {
            this.context = context;
            products = context.Products;
        }
        /// <summary>
        /// Метод для добавления нового продукта в бд
        /// </summary>
        /// <param name="entity">Объект бд</param>
        /// <returns>Новый объект из бд</returns>
        public async Task<Product> Create(Product entity)
        {
            await products.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            Product product = await products.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted==false);
            if (product is null) 
                throw new ArgumentNullException("Не наден продукт для удаления");
            product.IsDeleted = true;
            products.Update(product);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            List<Product> productsList = await products.Where(x=>x.IsDeleted==false).ToListAsync();
            if (productsList is null || !productsList.Any())
                throw new ArgumentNullException("В таблице отсутствуют товары");
            return productsList;
        }

        public async Task<Product> GetById(Guid id)
        {
            Product product = await products.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted==false);
            if (product is null)
                throw new ArgumentNullException("Такого прлдукта не существует");
            return product;
        }

        public async Task<IEnumerable<Product>> GetFiltered(Func<IQueryable<Product>, IQueryable<Product>> filter)
        {
            List<Product> productsList = await filter(products.Where(x=>x.IsDeleted==false).AsQueryable()).ToListAsync();
            if (productsList is not null && productsList.Any())
                return productsList;
            else
                throw new ArgumentNullException("Продукты с такими параметрами не найдены");
        }

        public async Task<Product> Update(Product entity)
        {
            products.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}

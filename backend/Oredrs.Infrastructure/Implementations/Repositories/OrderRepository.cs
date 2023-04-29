using Microsoft.EntityFrameworkCore;
using Oreders.Domain.Entity;
using Oreders.Domain.Enums;
using Oreders.Domain.Extensions;
using Oreders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Oredrs.Infrastructure.Implementations.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        readonly OrdersDbContext context;
        readonly DbSet<Order> orders;
        readonly DbSet<Product> products;
        readonly DbSet<Relation> relations;
        public OrderRepository(OrdersDbContext context)
        {
            this.context = context;
            this.orders = context.Oreders;
            this.products = context.Products;
            this.relations = context.Relations;
        }
        public async Task<Order> Create(Order entity)
        {
            await orders.AddAsync(entity);
            entity.Relations.ForEach(x=>x.OrderId = entity.Id);
            await relations.AddRangeAsync(entity.Relations);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            Order order = await orders.FirstOrDefaultAsync(o=> o.Id == id && o.IsDeleted==false);
            order.IsDeleted =true;
            orders.Update(order);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            List<Order> orderList = await orders.Where(o=>o.IsDeleted==false).Include(x=>x.Relations).ToListAsync();
            return orderList;
        }

        public async Task<Order> GetById(Guid id)
        {
            Order order = await orders.Include(x=>x.Relations).FirstOrDefaultAsync(x=>x.Id == id && !x.IsDeleted);
            return order;
        }

        /*public async Task<IEnumerable<Order>> GetFiltered(Func<IQueryable<Order>, IQueryable<Order>> filter)
        {
            List<Order> orderList = await filter.Invoke(orders.Where(x => x.IsDeleted == false).AsQueryable()).Include(x=>x.Relations).ToListAsync();
            if (orderList is not null && orderList.Any())
                return orderList;
            else
                throw new ArgumentNullException("Заказы с такими данными не найдены!");
        }*/

        public async Task<Order> Update(Order entity)
        {
            List<Relation> relationList = await relations.Where(x => x.OrderId == entity.Id).ToListAsync();
            relations.RemoveRange(relationList);
            orders.Update(entity);
            entity.Relations.ForEach(x => x.OrderId = entity.Id);
            await relations.AddRangeAsync(entity.Relations);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}

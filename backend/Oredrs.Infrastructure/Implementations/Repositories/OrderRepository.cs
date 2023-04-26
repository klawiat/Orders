using Microsoft.EntityFrameworkCore;
using Oreders.Domain.Entity;
using Oreders.Domain.Enums;
using Oreders.Domain.Extensions;
using Oreders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                if (!entity.Relations.Any())
                    throw new InvalidDataException("Заказ не может быть пустым");
                if (entity.Relations.Where(x => x.Count <= 0).Any())
                    throw new InvalidDataException("Количество не может быть меньше 1");
                entity.Status = Status.New;
                await orders.AddAsync(entity);
                foreach (var relation in entity.Relations)
                {
                    relation.OrderId = entity.Id;
                    if (await products.FirstOrDefaultAsync(x => x.Id == relation.ProductId) is null)
                        throw new InvalidDataException("Товар не найден");
                }
                await relations.AddRangeAsync(entity.Relations);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            Order order = await orders.FirstOrDefaultAsync(o=> o.Id == id && o.IsDeleted==false);
            if (order is null)
                throw new ArgumentNullException("Заказ не найден!");
            if ((int)order.Status >= (int)Status.SubmittedForDelivery)
                throw new InvalidOperationException($"Нельзя удалить заказ в статусе \"{order.Status.GetDisplayName()}\"");
            order.IsDeleted =true;
            orders.Update(order);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            List<Order> orderList = await orders.Where(o=>o.IsDeleted==false).Include(x=>x.Relations).ToListAsync();
            if (orderList is not null && orderList.Any())
                return orderList;
            else
                throw new ArgumentNullException("Заказы не найдены");
        }

        public async Task<Order> GetById(Guid id)
        {
            Order order = await orders.Include(x=>x.Relations).FirstOrDefaultAsync(x=>x.Id == id && x.IsDeleted==false);
            if (order is null)
                throw new ArgumentNullException("Заказ не найден");
            return order;
        }

        public async Task<IEnumerable<Order>> GetFiltered(Func<IQueryable<Order>, IQueryable<Order>> filter)
        {
            List<Order> orderList = await filter.Invoke(orders.Where(x => x.IsDeleted == false).AsQueryable()).Include(x=>x.Relations).ToListAsync();
            if (orderList is not null && orderList.Any())
                return orderList;
            else
                throw new ArgumentNullException("Заказы с такими данными не найдены!");
        }

        public async Task<Order> Update(Order entity)
        {
            Order order = await orders.FirstOrDefaultAsync(x=>x.Id==entity.Id && x.IsDeleted==false);
            if (order is null)
                throw new ArgumentNullException("Заказ не найден");
            if ((int)order.Status >= (int)Status.Paid)
                throw new InvalidOperationException($"Невозможно редактировать заказ в статусе \"{order.Status}\"");
            if (entity.Relations.Where(x => x.Count <= 0).Any())
                throw new InvalidOperationException("Нельзя добавить количество товаров меньше 0");
            entity.Created = order.Created;
            orders.Update(entity);
            foreach (var relation in entity.Relations)
            {
                relation.OrderId = entity.Id;
                if (await products.FirstOrDefaultAsync(x => x.Id == relation.ProductId) is null)
                    throw new ArgumentNullException("Товар не найден");
            }
            relations.UpdateRange(entity.Relations);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}

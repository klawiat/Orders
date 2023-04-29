using Microsoft.EntityFrameworkCore;
using Oreders.Domain.Entity;
using Oreders.Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Oredrs.Infrastructure
{
    public class OrdersDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Oreders { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public OrdersDbContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Установка связей для таблицы реализующей связь многие ко многим
            modelBuilder.Entity<Relation>().HasKey(e => new { e.ProductId, e.OrderId });
            modelBuilder.Entity<Relation>().HasOne(e => e.Order).WithMany(x => x.Relations).HasForeignKey(x=>x.OrderId);
            modelBuilder.Entity<Relation>().HasOne(e => e.Product).WithMany(x => x.Relations).HasForeignKey(x => x.ProductId);

            //Установка свойств таблицы заказов
            modelBuilder.Entity<Order>().Property(x=>x.Created).HasDefaultValueSql("timezone('UTC', now())")/*.HasDefaultValueSql("timezone('utc', now())").IsRequired()*/;
            modelBuilder.Entity<Order>().Property(x=>x.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Order>().Property(x=>x.Status).HasDefaultValue(Status.New);

            //
            modelBuilder.Entity<Product>().Property(x => x.IsDeleted).HasDefaultValue(false);


            //
            Guid guidProduct = Guid.NewGuid();
            modelBuilder.Entity<Product>().HasData(
                    new Product() { Id=guidProduct,Name="Ложки"},
                    new Product() { Id = Guid.NewGuid(), Name = "Вилки" },
                    new Product() { Id = Guid.NewGuid(), Name = "Ножи" }
                );
        }
    }
}
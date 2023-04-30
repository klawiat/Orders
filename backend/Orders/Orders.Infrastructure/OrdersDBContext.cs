using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Enums;

namespace Orders.Infrastructure
{
    public class OrdersDBContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> products { get; set; }

        public OrdersDBContext(DbContextOptions<OrdersDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Конфигурация таблицы заказов
            modelBuilder.Entity<Order>()
                .Property(order => order.Status)
                .HasConversion(staus => staus.ToString(), status => Enum.Parse<Status>(status, true));
            modelBuilder.Entity<Order>()
                .Property(order => order.Status)
                .HasDefaultValue(Status.New);
            modelBuilder.Entity<Order>()
                .Property(order => order.Created)
                .HasDefaultValueSql("timezone('UTC', now())");
            //Конфигурация таблицы позиций в заказе
            modelBuilder.Entity<OrderItem>()
                .HasOne(item => item.Order)
                .WithMany(order => order.Lines);
            modelBuilder.Entity<OrderItem>()
                .HasIndex(item => new { item.OrderId, item.ProductId })
                .IsUnique();
        }
    }
}

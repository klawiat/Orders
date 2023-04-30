using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Interfaces.Services;
using Orders.Application.Services;
using Orders.Domain.Contracts.Repositories;
using Orders.Infrastructure.Implementations.Repository;
using System.Reflection;

namespace Orders.Application
{
    public static class Initialitializer
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IOrderService, OrderService>();
        }
        public static void AddInfastructure(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}

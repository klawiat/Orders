﻿using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Services;
using Oreders.Domain.Entity;
using Oreders.Domain.Interfaces;
using Oreders.Domain.Interfaces.Services;
using Oredrs.Infrastructure.Implementations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Initialize
{
    public static class Initializer
    {
        public static string GetStr()
        {
            return "";
        }
        public static void InitializeRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Product>,ProductRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
        }
        public static void InitializeServices (this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService,OrderService>();
        }
    }
}

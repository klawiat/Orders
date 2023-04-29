using Microsoft.EntityFrameworkCore;
using Orders.Application.Initialize;
using Orders.WebApi.Models;
using Oredrs.Infrastructure;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Oreders.Domain.Entity;
using Swashbuckle.Swagger.Annotations;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Globalization;
using Orders.WebApi.Converters;
using Orders.Application.Models;

namespace Orders.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddJsonOptions(opt=>opt.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));
			#region Строка подключения
            string host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
            string port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
            string database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "OrdersDB";
            string username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
            string password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "Klawiat1324";
            string connection = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
			#endregion
            builder.Services.AddDbContext<OrdersDbContext>(
                    opt => 
                    { 
                        opt.UseNpgsql(connection);
                        opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    },
                    ServiceLifetime.Scoped
                );
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.AddProfile(new ApplicationProfile());
            });
            builder.Services.InitializeRepository();
            builder.Services.InitializeServices();
            builder.Services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Version = "v1", });
            });
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Orders"));
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",()=>Results.Redirect("/swagger"));
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Products}/{action=All}/{id?}"
                    );
            });
            /*app.MapControllerRoute(
            name: "default",
                    pattern: "{controller=Orders}/{Action=all}/{id?}"
                );*/

            app.Run();
        }
    }
}
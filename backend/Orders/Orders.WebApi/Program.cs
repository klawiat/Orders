using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Orders.Application;
using Orders.Application.Models.Mapping;
using Orders.Infrastructure;
using Orders.WebApi.Converters;
using Orders.WebApi.Middleware;
using Orders.WebApi.Models.MappingProfiles;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Orders.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            #region Строка подключения к бд
            string host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
            string port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
            string database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "OrdersTestTask";
            string username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
            string password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "Klawiat1324";
            string connection = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
            connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? connection;
            #endregion
            #region Подключение бд
            builder.Services.AddDbContext<OrdersDBContext>(
                cfg =>
                {
                    cfg.UseNpgsql(connection);
                },
                ServiceLifetime.Scoped);
            #endregion
            builder.Services.AddInfastructure();
            builder.Services.AddApplication();
            builder.Services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new DateTimeToUTCConverter());
                opt.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                opt.JsonSerializerOptions.WriteIndented = true;
            });
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<OrederItemViewModelProfile>();
                cfg.AddProfile<OrderViewModelsProfile>();
                cfg.AddProfile<OrderDTOProfile>();
                cfg.AddProfile<OrderItemDTOProfile>();
            });
            builder.Services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Version = "v1", });
            });
            WebApplication app = builder.Build();
            app.UseExeptionHandlerMiddleware();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Orders"));
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}
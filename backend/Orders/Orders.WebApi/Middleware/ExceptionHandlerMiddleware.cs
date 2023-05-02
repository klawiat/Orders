using AutoMapper;
using FluentValidation;
using Orders.Application.Exceptions;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using json = System.Text.Json.JsonSerializer;

namespace Orders.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        #region Свойства
        private readonly RequestDelegate _next;
        private readonly JsonSerializerOptions jsonOptions;
        #endregion
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            jsonOptions = new JsonSerializerOptions();
            jsonOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            jsonOptions.WriteIndented = true;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string responce = string.Empty;
            switch (exception)
            {
                case ValidationException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responce = json.Serialize(ex.Errors.Select(error => new { property = error.PropertyName, message = error.ErrorMessage }), jsonOptions);
                    break;
                case ConflictException ex:
                    statusCode = HttpStatusCode.Conflict;
                    responce = json.Serialize(new { message = ex.Message }, jsonOptions);
                    break;
                case ArgumentNullException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responce = json.Serialize(new { message = "Данные не соответствуют контракту" }, jsonOptions);
                    break;
                case InvalidOperationException ex:
                    statusCode = HttpStatusCode.Forbidden;
                    responce = json.Serialize(new { message = ex.Message });
                    break;
                case NoDataException ex:
                    statusCode = HttpStatusCode.NotFound;
                    responce = json.Serialize(new { message = ex.Message });
                    break;
                case NullReferenceException:
                    statusCode = HttpStatusCode.NotFound;
                    responce = json.Serialize(new { message = "Запрашиваемые данные не найдены" }, jsonOptions);
                    break;
                case AutoMapperMappingException:
                    statusCode = HttpStatusCode.BadRequest;
                    responce = json.Serialize(new { message = "Неверный формат данных" });
                    break;
                case FormatException:
                    statusCode = HttpStatusCode.BadRequest;
                    responce = json.Serialize(new { message = "Не удалось распознать данные" });
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    responce = json.Serialize(new { message = "Непредвиденная ошибка" }, jsonOptions);
                    break;
            }
            context.Response.StatusCode = (int)statusCode;
            context.Response.WriteAsync(responce);
            return Task.CompletedTask;
        }
    }
    public static class ExceptionHandelerExtension
    {
        public static void UseExeptionHandlerMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

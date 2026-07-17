using System.Net;
using System.Text.Json;

namespace Billar306.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado en {Path}", context.Request.Path);
                await ManejarExcepcionAsync(context, ex);
            }
        }

        private static Task ManejarExcepcionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var respuesta = JsonSerializer.Serialize(new
            {
                mensaje = "Ocurrió un error inesperado. Revisá tu conexión Wi-Fi o avisá al encargado.",
                detalle = ex.Message
            });

            return context.Response.WriteAsync(respuesta);
        }
    }
}
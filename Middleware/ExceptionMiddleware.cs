using System.Net;
using System.Text.Json;

namespace PerezTravelToursAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Ejecutar la siguiente parte del pipeline
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Registrar el error en los logs
                _logger.LogError(
                    ex,
                    "Se produjo una excepción no controlada."
                );

                // Manejar la excepción
                await HandleExceptionAsync(
                    httpContext,
                    ex
                );
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext httpContext,
            Exception exception)
        {
            // Configurar código de respuesta
            httpContext.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            // Indicar que la respuesta será JSON
            httpContext.Response.ContentType =
                "application/json";

            // Crear respuesta
            var response = new
            {
                statusCode = httpContext.Response.StatusCode,

                mensaje = "Ha ocurrido un error interno en el servidor.",

                detalle = exception.Message
            };

            // Convertir respuesta a JSON
            var jsonResponse = JsonSerializer.Serialize(response);

            // Enviar respuesta
            await httpContext.Response.WriteAsync(
                jsonResponse
            );
        }
    }
}

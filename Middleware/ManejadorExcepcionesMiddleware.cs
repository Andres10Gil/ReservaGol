using System.Net;
using System.Text.Json;

namespace ReservaGol.Middleware
{
    // Captura cualquier excepción no controlada en el pipeline y la convierte
    // en una respuesta 500 uniforme, evitando repetir try/catch en cada controlador.
    public class ManejadorExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorExcepcionesMiddleware> _logger;

        public ManejadorExcepcionesMiddleware(RequestDelegate next, ILogger<ManejadorExcepcionesMiddleware> logger)
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
                _logger.LogError(ex, "Error no controlado procesando {Metodo} {Ruta}", context.Request.Method, context.Request.Path);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var respuesta = JsonSerializer.Serialize(new { mensaje = "Ocurrió un error inesperado. Intenta nuevamente más tarde." });
                await context.Response.WriteAsync(respuesta);
            }
        }
    }

    public static class ManejadorExcepcionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseManejadorExcepciones(this IApplicationBuilder app) =>
            app.UseMiddleware<ManejadorExcepcionesMiddleware>();
    }
}

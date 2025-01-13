using System.Diagnostics;

namespace MaintenancePrediction.ApiService.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unhandled Exception: {ex.Message}");
                throw;
            }
        }
    }

}

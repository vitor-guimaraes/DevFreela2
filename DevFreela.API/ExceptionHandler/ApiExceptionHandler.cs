using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevFreela.API.ExceptionHandler
{
    public class ApiExceptionHandler : IExceptionHandler
    {
        async ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error"
            };


            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(details);

            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }
    }
}

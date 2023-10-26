using Entity.ErrorModel;
using Entity.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace PosSystem.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, Serilog.ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {

                    // Default StatusCode
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    // Json Type Response
                    context.Response.ContentType = "application/json";
                    // Access the specific Exception
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            ConflictExeception => StatusCodes.Status409Conflict,
                            BadRequestException => StatusCodes.Status400BadRequest, 
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}

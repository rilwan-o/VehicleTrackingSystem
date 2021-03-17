using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using VehicleTrackingSystem.API.DTO;
using VehicleTrackingSystem.API.Enumerations;

namespace VehicleTrackingSystem.API.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ApiResponse()
                        {
                            Code = ResponseEnum.SystemMalfunction.ResponseCode(),
                            Description = ResponseEnum.SystemMalfunction.DisplayName()
                        }.ToString());
                    }
                });
            });
        }
    }
}

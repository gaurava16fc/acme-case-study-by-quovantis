using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ACME.Backend.API.Helper
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureUnhandledExceptionsHandler (this IApplicationBuilder app)
        {
            // Extension Method# The below section is used to handle Unhandled Exceptions at global level in ASP.NET Core apps
            #region Handle Unhandled Exceptions at global level
            app.UseExceptionHandler(builder => {
                builder.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        // Added Application error via creating an exception method (i.e. AddApplicationError())
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });
            #endregion
        }
    }
}

using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Pro.Modular.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseMiddleware(this WebApplication app)
    {
        app.UseStatusCodePages();

        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.ContentType = "application/problem+json";

                if (context.RequestServices.GetService<IProblemDetailsService>() is { } problemDetailsService)
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exceptionType = exceptionHandlerFeature?.Error;

                    if (exceptionType is not null)
                    {
                        (string Title, string Detail, int StatusCode) details = exceptionType switch
                        {
                            CustomException customException =>
                            (
                                exceptionType.GetType().Name,
                                exceptionType.Message,
                                context.Response.StatusCode = (int)customException.StatusCode
                            ),
                            _ =>
                            (
                                exceptionType.GetType().Name,
                                exceptionType.Message,
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                            )
                        };

                        var problem = new ProblemDetailsContext
                        {
                            HttpContext = context,
                            ProblemDetails =
                            {
                                Title = details.Title,
                                Detail = details.Detail,
                                Status = details.StatusCode
                            }
                        };

                        if (app.Environment.IsDevelopment())
                            problem.ProblemDetails.Extensions.Add("exception",
                                exceptionHandlerFeature?.Error.ToString());

                        await problemDetailsService.WriteAsync(problem);
                    }
                }
            });
        });

        // if (app.Environment.IsDevelopment())
        //     app.UseDeveloperExceptionPage();
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseHsts();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwaggerEndpoint();

        return app;
    }

    private static WebApplication UseSwaggerEndpoint(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
            options.OAuthClientId(
                app.Configuration["SwaggerJwtOptions:OpenIdClientId"]);
            options.OAuthUsePkce();
            options.OAuthScopeSeparator(" ");
        });

        return app;
    }

    public class CustomException : Exception
    {
        public CustomException(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
        ) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
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
                        var problem = BuildProblemDetails(app, exceptionType, context, exceptionHandlerFeature);

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

    private static ProblemDetailsContext BuildProblemDetails(WebApplication app, Exception exceptionType,
        HttpContext context, IExceptionHandlerFeature? exceptionHandlerFeature)
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

        return problem;
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

    private class CustomException : Exception
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
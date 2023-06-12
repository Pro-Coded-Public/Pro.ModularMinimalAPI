using Pro.Modular.Shared.Models;

namespace Pro.Modular.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseMiddleware(this WebApplication app)
    {
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        return app;
    }

    public static WebApplication UseErrorHandling(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(exceptionHandlerApp
                => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
            app.UseStatusCodePages();
        }

        return app;
    }

    public static WebApplication UseSwaggerEndpoint(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
            options.OAuthClientId(
                app.Configuration["JwtOptions:OpenIdClientId"]);
            options.OAuthUsePkce();
            options.OAuthScopeSeparator(" ");
        });

        return app;
    }
}
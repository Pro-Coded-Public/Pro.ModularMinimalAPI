using System.Reflection;
using CQRSModule.Data;
using CQRSModule.Features.Students.Create;
using CQRSModule.Features.Students.Delete;
using CQRSModule.Features.Students.GetAll;
using CQRSModule.Features.Students.GetById;
using CQRSModule.Features.Students.GetByName;
using CQRSModule.Features.Students.Update;
using CQRSModule.Services;
using CQRSModule.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Pro.Modular.Shared.Interfaces;

namespace CQRSModule;

public class Module : IModule
{
    public string ModuleName => "SecureExampleModule";

    public string SettingsFileName => "secureExampleAppSettings.json";

    public WebApplicationBuilder AddModuleServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DataContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlite(connectionString,
                opt => { opt.CommandTimeout((int)TimeSpan.FromSeconds(60).TotalSeconds); });
        });


        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                Assembly.GetExecutingAssembly());
        });

        builder.Services.AddTransient<IStudentsRepository, StudentsRepository>();
        builder.Services.AddScoped<IStudentsService, StudentsService>();

        return builder;
    }

    public WebApplication UseModuleMiddleware(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DataContext>();

        context.Database.Migrate();
        DataSeeder.Seed(context);

        return app;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var cqrsExamples = endpoints.MapGroup("/SecureExamples")
            .WithTags("CQRS Examples")
            .WithOpenApi();
//            .RequireAuthorization("ApiKeyPolicy");

        endpoints.MapGetAllStudents()
            .MapGetByIdStudent()
            .MapGetByNameStudent()
            .MapPostCreateStudent()
            .MapPutUpdateStudent()
            .MapDeleteStudent();

        return endpoints;
    }

    public void BindOptions(WebApplicationBuilder builder)
    {
    }
}
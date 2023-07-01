using CQRSModule.Features.Students.Models;
using CQRSModule.Services;
using MiniValidation;

namespace CQRSModule.Features.Students.Create;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapPostCreateStudent(this IEndpointRouteBuilder app)
    {
        app.MapPost("student/create", async (Student student, IStudentsService studentService) =>
            !MiniValidator.TryValidate(student, out var errors)
                ? Results.ValidationProblem(errors)
                : Results.Ok(await studentService.Create(student).ConfigureAwait(false)));
        return app;
    }
}
using MediatR;
using MiniValidation;

namespace CQRSModule.Features.Students.Create;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapPostCreateStudent(this IEndpointRouteBuilder app)
    {
        app.MapPost("student/create", async (CreateStudentCommand createStudentCommand, IMediator mediatr) =>
            !MiniValidator.TryValidate(createStudentCommand, out var errors)
                ? Results.ValidationProblem(errors)
                : Results.Ok(await mediatr.Send(createStudentCommand).ConfigureAwait(false)));
        return app;
    }
}
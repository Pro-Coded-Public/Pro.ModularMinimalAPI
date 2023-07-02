using MediatR;

namespace CQRSModule.Features.Students.Delete;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapDeleteStudent(this IEndpointRouteBuilder app)
    {
        app.MapDelete("student/delete", async (int id, IMediator mediator) =>
        {
            try
            {
                var success = await mediator.Send(new DeleteStudentCommand { Id = id }).ConfigureAwait(false);
                return success ? Results.Ok() : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}
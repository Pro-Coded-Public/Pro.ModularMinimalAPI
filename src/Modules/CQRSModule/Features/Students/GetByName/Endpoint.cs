using MediatR;

namespace CQRSModule.Features.Students.GetByName;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapGetByNameStudent(this IEndpointRouteBuilder app)
    {
        app.MapGet("student/get-by-name", async (string name, IMediator mediator) =>
        {
            try
            {
                var existingStudent =
                    await mediator.Send(new GetStudentByNameQuery(name)).ConfigureAwait(false);
                return existingStudent != null ? Results.Ok(existingStudent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}
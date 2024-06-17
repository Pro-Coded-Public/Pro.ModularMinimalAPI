using MediatR;

namespace CQRSModule.Features.Students.GetAll;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapGetAllStudents(this IEndpointRouteBuilder app)
    {
        app.MapGet("student/get-all", async (IMediator mediator) =>
        {
            try
            {
                var existingStudents = await mediator.Send(new GetAllStudentsQuery()).ConfigureAwait(false);
                return existingStudents != null ? Results.Ok(existingStudents) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}
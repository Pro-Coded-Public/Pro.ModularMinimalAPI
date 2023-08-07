using MediatR;

namespace CQRSModule.Features.Students.GetById;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapGetByIdStudent(this IEndpointRouteBuilder app)
    {
        app.MapGet("student/get-by-id", async (int id, IMediator mediator) =>
        {
            try
            {
                var existingStudent = await mediator.Send(new GetStudentByIdQuery(id)).ConfigureAwait(false);
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
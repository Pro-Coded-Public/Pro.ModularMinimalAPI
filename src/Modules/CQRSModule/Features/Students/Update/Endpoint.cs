using MediatR;

namespace CQRSModule.Features.Students.Update;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapPutUpdateStudent(this IEndpointRouteBuilder app)
    {
        app.MapPut("student/update", async (UpdateStudentCommand updateStudentCommand, IMediator mediator) =>
        {
            try
            {
                var updatedStudent = await mediator.Send(updateStudentCommand).ConfigureAwait(false);
                return Results.Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}
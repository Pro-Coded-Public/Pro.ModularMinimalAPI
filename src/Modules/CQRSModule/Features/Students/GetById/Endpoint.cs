using CQRSModule.Services;

namespace CQRSModule.Features.Students.GetById;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapGetByIdStudent(this IEndpointRouteBuilder app)
    {
        app.MapGet("student/get-by-id", async (int id, IStudentsService studentService) =>
        {
            try
            {
                var existingStudent = await studentService.GetById(id).ConfigureAwait(false);
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
using CQRSModule.Services;

namespace CQRSModule.Features.Students.GetByName;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapGetByNameStudent(this IEndpointRouteBuilder app)
    {
        app.MapGet("student/get-by-name", async (string name, IStudentsService studentService) =>
        {
            try
            {
                var existingStudent = await studentService.GetByName(name).ConfigureAwait(false);
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
using CQRSModule.Services;

namespace CQRSModule.Endpoints.Students.GetAll;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapGetAllStudents(this IEndpointRouteBuilder app)
    {
        app.MapGet("student/get-all", async (IStudentsService studentService) =>
        {
            try
            {
                var existingStudents = await studentService.GetAll().ConfigureAwait(false);
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
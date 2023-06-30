using CQRSModule.Models;
using CQRSModule.Services;

namespace CQRSModule.Endpoints.Students.Update;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapPutUpdateStudent(this IEndpointRouteBuilder app)
    {
        app.MapPut("student/update", async (Student student, IStudentsService studentService) =>
        {
            try
            {
                var updatedStudent = await studentService.Update(student).ConfigureAwait(false);
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
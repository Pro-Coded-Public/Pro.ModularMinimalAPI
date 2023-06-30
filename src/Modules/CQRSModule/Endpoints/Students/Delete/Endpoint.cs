using CQRSModule.Services;

namespace CQRSModule.Endpoints.Students.Delete;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapDeleteStudent(this IEndpointRouteBuilder app)
    {
        app.MapDelete("student/delete", async (int id, IStudentsService studentService) =>
        {
            try
            {
                var success = await studentService.Delete(id).ConfigureAwait(false);
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
using MediatR;

namespace CQRSModule.Features.Students.Delete;

public class DeleteStudentCommand : IRequest<bool>
{
    public int Id { get; set; }
}
using CQRSModule.Services;
using MediatR;

namespace CQRSModule.Features.Students.Delete;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentsService _studentsService;

    public DeleteStudentCommandHandler(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    public async Task<bool> Handle(DeleteStudentCommand deleteStudentCommand,
        CancellationToken cancellationToken)
    {
        return await _studentsService.Delete(deleteStudentCommand.Id, cancellationToken);
    }
}
using CQRSModule.Features.Students.Models;
using CQRSModule.Services;
using MediatR;

namespace CQRSModule.Features.Students.Update;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Student?>
{
    private readonly IStudentsService _studentsService;

    public UpdateStudentCommandHandler(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    public async Task<Student?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student(request.Name, request.Address, request.Email, request.DateOfBirth, request.Active)
        {
            Id = request.Id
        };
        return await _studentsService.Update(student, cancellationToken).ConfigureAwait(false);
    }
}
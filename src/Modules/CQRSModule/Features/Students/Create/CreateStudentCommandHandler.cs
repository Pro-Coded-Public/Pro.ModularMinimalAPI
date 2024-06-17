using CQRSModule.Features.Students.Models;
using CQRSModule.Services;
using MediatR;

namespace CQRSModule.Features.Students.Create;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Student>
{
    private readonly IStudentsService _studentsService;

    public CreateStudentCommandHandler(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    public async Task<Student> Handle(CreateStudentCommand createStudentCommand,
        CancellationToken cancellationToken)
    {
        var newStudent = new Student(createStudentCommand.Name, createStudentCommand.Address,
            createStudentCommand.Email, createStudentCommand.DateOfBirth);

        return await _studentsService.Create(newStudent, cancellationToken);
    }
}
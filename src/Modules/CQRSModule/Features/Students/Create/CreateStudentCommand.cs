using CQRSModule.Features.Students.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Features.Students.Create;

public class CreateStudentCommand : IRequest<Student>
{
    public string Name { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public bool? Active { get; set; } = true;
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Student>
{
    private readonly IStudentsRepository _studentsRepository;

    public CreateStudentCommandHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var newStudent = new Student(request.Name, request.Address, request.Email, request.DateOfBirth);
        return await _studentsRepository.AddAsync(newStudent, cancellationToken).ConfigureAwait(false);
    }
}
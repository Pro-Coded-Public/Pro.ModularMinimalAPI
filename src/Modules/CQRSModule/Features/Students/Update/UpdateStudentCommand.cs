using CQRSModule.Features.Students.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Features.Students.Update;

public class UpdateStudentCommand : IRequest<Student>
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public bool? Active { get; set; } = true;
}

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Student?>
{
    private readonly IStudentsRepository _studentsRepository;

    public UpdateStudentCommandHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student(request.Name, request.Address, request.Email, request.DateOfBirth, request.Active)
        {
            Id = request.Id
        };
        return await _studentsRepository.UpdateAsync(student, cancellationToken).ConfigureAwait(false);
    }
}
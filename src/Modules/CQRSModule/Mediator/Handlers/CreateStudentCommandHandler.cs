using CQRSModule.Mediator.Commands;
using CQRSModule.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Mediator.Handlers;

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
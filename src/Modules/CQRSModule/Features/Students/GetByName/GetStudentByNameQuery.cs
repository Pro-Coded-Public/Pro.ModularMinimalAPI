using CQRSModule.Features.Students.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Features.Students.GetByName;

public class GetStudentByNameQuery : IRequest<Student>
{
    public string Name { get; set; }
}

public class GetStudentByNameQueryHandler : IRequestHandler<GetStudentByNameQuery, Student?>
{
    private readonly IStudentsRepository _studentsRepository;

    public GetStudentByNameQueryHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student?> Handle(GetStudentByNameQuery request, CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetByNameAsync(request.Name, cancellationToken).ConfigureAwait(false);
    }
}
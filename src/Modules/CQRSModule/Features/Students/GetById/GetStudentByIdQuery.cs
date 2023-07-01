using CQRSModule.Features.Students.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Features.Students.GetById;

public class GetStudentByIdQuery : IRequest<Student>
{
    public int Id { get; set; }
}

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student?>
{
    private readonly IStudentsRepository _studentsRepository;

    public GetStudentByIdQueryHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
    }
}
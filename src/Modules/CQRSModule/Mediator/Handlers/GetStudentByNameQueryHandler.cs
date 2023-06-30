using CQRSModule.Mediator.Queries;
using CQRSModule.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Mediator.Handlers;

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
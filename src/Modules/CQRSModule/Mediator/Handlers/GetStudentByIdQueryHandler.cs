using CQRSModule.Mediator.Queries;
using CQRSModule.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Mediator.Handlers;

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
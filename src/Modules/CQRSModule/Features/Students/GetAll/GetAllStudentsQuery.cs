using CQRSModule.Features.Students.Models;
using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Features.Students.GetAll;

public class GetAllStudentsQuery : IRequest<IList<Student>>
{
}

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IList<Student>?>
{
    private readonly IStudentsRepository _studentsRepository;

    public GetAllStudentsQueryHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<IList<Student>?> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
    }
}
using CQRSModule.Features.Students.Models;
using CQRSModule.Services;
using MediatR;

namespace CQRSModule.Features.Students.GetAll;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IList<Student>?>
{
    private readonly IStudentsService _studentsService;

    public GetAllStudentsQueryHandler(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    public async Task<IList<Student>?> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _studentsService.GetAll(cancellationToken).ConfigureAwait(false);
    }
}
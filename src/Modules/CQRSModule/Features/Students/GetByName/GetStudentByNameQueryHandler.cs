using CQRSModule.Features.Students.Models;
using CQRSModule.Services;
using MediatR;

namespace CQRSModule.Features.Students.GetByName;

public class GetStudentByNameQueryHandler : IRequestHandler<GetStudentByNameQuery, Student?>
{
    private readonly IStudentsService _studentsService;

    public GetStudentByNameQueryHandler(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    public async Task<Student?> Handle(GetStudentByNameQuery request, CancellationToken cancellationToken)
    {
        return await _studentsService.GetByName(request.Name, cancellationToken).ConfigureAwait(false);
    }
}
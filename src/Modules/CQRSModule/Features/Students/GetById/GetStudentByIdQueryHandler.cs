using CQRSModule.Features.Students.Models;
using CQRSModule.Services;
using MediatR;

namespace CQRSModule.Features.Students.GetById;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student?>
{
    private readonly IStudentsService _studentsService;

    public GetStudentByIdQueryHandler(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    public async Task<Student?> Handle(GetStudentByIdQuery getStudentByIdQuery, CancellationToken cancellationToken)
    {
        return await _studentsService.GetById(getStudentByIdQuery.Id, cancellationToken);
    }
}
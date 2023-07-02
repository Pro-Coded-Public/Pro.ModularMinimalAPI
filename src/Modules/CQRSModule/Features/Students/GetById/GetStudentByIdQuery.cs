using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.GetById;

public class GetStudentByIdQuery : IRequest<Student>
{
    public int Id { get; set; }
}
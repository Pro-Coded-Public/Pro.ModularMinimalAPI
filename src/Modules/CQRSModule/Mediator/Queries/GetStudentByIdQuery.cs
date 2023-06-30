using CQRSModule.Models;
using MediatR;

namespace CQRSModule.Mediator.Queries;

public class GetStudentByIdQuery : IRequest<Student>
{
    public int Id { get; set; }
}
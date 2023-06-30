using CQRSModule.Models;
using MediatR;

namespace CQRSModule.Mediator.Queries;

public class GetStudentByNameQuery : IRequest<Student>
{
    public string Name { get; set; }
}
using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.GetByName;

public class GetStudentByNameQuery : IRequest<Student>
{
    public string Name { get; set; }
}
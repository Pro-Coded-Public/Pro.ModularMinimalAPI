using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.GetAll;

public record GetAllStudentsQuery : IRequest<IList<Student>>
{
}
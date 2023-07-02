using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.GetAll;

public class GetAllStudentsQuery : IRequest<IList<Student>>
{
}
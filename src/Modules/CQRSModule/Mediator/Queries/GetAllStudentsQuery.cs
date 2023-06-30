using CQRSModule.Models;
using MediatR;

namespace CQRSModule.Mediator.Queries;

public class GetAllStudentsQuery : IRequest<IList<Student>>
{
}
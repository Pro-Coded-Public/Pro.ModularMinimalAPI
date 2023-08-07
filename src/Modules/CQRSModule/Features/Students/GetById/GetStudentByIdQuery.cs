using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.GetById;

public record GetStudentByIdQuery(int Id) : IRequest<Student>;
using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.GetByName;

public record GetStudentByNameQuery(string Name) : IRequest<Student>;
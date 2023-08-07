using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.Create;

public record CreateStudentCommand(string? Name,
    string? Address,
    string? Email,
    DateTime? DateOfBirth,
    bool? Active) : IRequest<Student>;
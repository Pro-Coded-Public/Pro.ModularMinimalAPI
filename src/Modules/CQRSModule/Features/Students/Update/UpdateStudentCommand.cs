using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.Update;

public record UpdateStudentCommand(int Id,
    string? Name,
    string? Address,
    string? Email,
    DateTime? DateOfBirth,
    bool? Active) : IRequest<Student>;
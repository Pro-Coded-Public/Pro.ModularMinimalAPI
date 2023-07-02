using CQRSModule.Features.Students.Models;
using MediatR;

namespace CQRSModule.Features.Students.Update;

public class UpdateStudentCommand : IRequest<Student>
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public bool? Active { get; set; } = true;
}
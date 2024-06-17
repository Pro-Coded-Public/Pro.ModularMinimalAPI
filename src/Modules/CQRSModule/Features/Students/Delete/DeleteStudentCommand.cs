using MediatR;

namespace CQRSModule.Features.Students.Delete;

public record DeleteStudentCommand(int Id) : IRequest<bool>;
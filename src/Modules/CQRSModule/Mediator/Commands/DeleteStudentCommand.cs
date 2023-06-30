using MediatR;

namespace CQRSModule.Mediator.Commands;

public class DeleteStudentCommand : IRequest<bool>
{
    public int Id { get; set; }
}
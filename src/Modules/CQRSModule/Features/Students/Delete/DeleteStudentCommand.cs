using CQRSModule.Services.Repositories;
using MediatR;

namespace CQRSModule.Features.Students.Delete;

public class DeleteStudentCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentsRepository _studentsRepository;

    public DeleteStudentCommandHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        return await _studentsRepository.RemoveAsync(request.Id, cancellationToken).ConfigureAwait(false);
    }
}
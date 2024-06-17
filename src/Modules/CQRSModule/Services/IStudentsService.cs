using CQRSModule.Features.Students.Models;

namespace CQRSModule.Services;

public interface IStudentsService
{
    Task<bool> Delete(int id, CancellationToken cancellationToken);

    Task<IList<Student>?> GetAll(CancellationToken cancellationToken);

    Task<Student?> GetById(int id, CancellationToken cancellationToken);

    Task<Student?> GetByName(string name, CancellationToken cancellationToken);

    Task<Student> Create(Student student, CancellationToken cancellationToken);

    Task<Student> Update(Student student, CancellationToken cancellationToken);
}
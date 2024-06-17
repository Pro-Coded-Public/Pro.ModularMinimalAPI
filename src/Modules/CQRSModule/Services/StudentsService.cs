using CQRSModule.Features.Students.Models;
using CQRSModule.Services.Repositories;

namespace CQRSModule.Services;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _studentsRepository;

    public StudentsService(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student> Update(Student student, CancellationToken cancellationToken)
    {
        return await _studentsRepository.UpdateAsync(student, cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _studentsRepository.RemoveAsync(id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Student> Create(Student student, CancellationToken cancellationToken)
    {
        return await _studentsRepository.AddAsync(student, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IList<Student>?> GetAll(CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Student?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Student?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetByNameAsync(name, cancellationToken).ConfigureAwait(false);
    }
}
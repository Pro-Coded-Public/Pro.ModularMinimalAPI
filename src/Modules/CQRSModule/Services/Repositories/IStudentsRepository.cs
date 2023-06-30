using CQRSModule.Models;
using CQRSModule.Services.Repositories.Base;

namespace CQRSModule.Services.Repositories;

public interface IStudentsRepository : IBaseRepository<Student>
{
    Task<IList<Student>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Student?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
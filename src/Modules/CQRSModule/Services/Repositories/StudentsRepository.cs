using CQRSModule.Data;
using CQRSModule.Features.Students.Models;
using CQRSModule.Services.Repositories.Base;

namespace CQRSModule.Services.Repositories;

public class StudentsRepository : BaseRepository<Student, DataContext>, IStudentsRepository
{
    public StudentsRepository(DataContext? dbContext)
        : base(dbContext)
    {
    }

    public async Task<IList<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await GetAsync<Student>(orderBy: cmp => cmp.OrderBy(std => std.Name),
            cancellationToken: cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetSingleOrDefaultAsync<Student>(std => std.Id == id, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Student?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await GetSingleOrDefaultAsync<Student>(std => std.Name == name, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
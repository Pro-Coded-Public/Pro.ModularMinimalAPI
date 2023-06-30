using CQRSModule.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSModule.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Student>? Students { get; set; }
}
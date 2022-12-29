using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DataCollectionTask> DataCollectionTasks { get; set; }
}
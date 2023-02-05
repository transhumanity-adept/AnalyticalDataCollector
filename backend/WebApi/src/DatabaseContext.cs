using Microsoft.EntityFrameworkCore;

using WebApi.Features.Articles.Models;
using WebApi.Features.Tasks.Models;

namespace WebApi;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options) { }

    public DbSet<DataCollectionTaskGeneral> DataCollectionTasks { get; set; }

    public DbSet<IntervalDataCollectionTask> IntervalDataCollectionTasks { get; set; }

    public DbSet<DataCollectionTaskLog> DataCollectionTaskLogs { get; set; }

    public DbSet<Article> Articles { get; set; }

    public DbSet<ArticleAuthor> ArticleAuthors { get; set; }

    public DbSet<ArticleOrganization> ArticleOrganizations { get; set; }

    public DbSet<ArticleKeyWord> ArticleKeyWords { get; set; }
}
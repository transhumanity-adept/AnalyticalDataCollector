namespace WebApi;

public static class DatabaseInitializer
{
    public static void Initialize(DatabaseContext context)
    {
        context.Database.EnsureCreated();
    }
}
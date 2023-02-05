using System.Reflection;
using FluentValidation;
using MediatR;
using WebApi;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.Tasks.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddSignalR();

#region AddServices
builder.Services.AddSingleton<IBackgroundDataCollectionTaskService, BackgroundDataCollectionTaskService>();
#endregion


builder.Services.AddDbContext<DatabaseContext>(
    optBuilder => optBuilder.UseNpgsql(
            builder.Environment.IsDevelopment() ?
                Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                : Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_DEVELOPMENT")
        )
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(opt =>
{
    opt.MapControllers();
    opt.MapHub<ApplicationHub>("/backendhub");
});

app.UseSwagger();
app.UseSwaggerUI();

// Initializing database
{
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        DatabaseInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return;
    }
}

// Create singleton services

app.Run();

using WebApi;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IDataCollectionTaskManager, DataCollectionTaskManager>();
builder.Services.AddScoped<IDataCollectionTaskService, DataCollectionTaskService>();
builder.Services.AddDbContext<DatabaseContext>(optBuilder => optBuilder.UseSqlite("Filename=store.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Services.GetService<IDataCollectionTaskManager>();

app.UseRouting();
app.UseEndpoints(opt => opt.MapControllers());

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

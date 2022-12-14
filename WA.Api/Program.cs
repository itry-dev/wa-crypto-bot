using Microsoft.EntityFrameworkCore;
using WA.Infrastructure;
using WA.Infrastructure.Config.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddWACoreConfig();

//TODO too tied to the previous call that sets ApiProviderConfig
builder.Services.AddWACoreServices();

builder.WebHost.UseUrls("http://localhost:5500");

builder.Services.AddDbContext<SqliteDataContext>();

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SqliteDataContext>();
    dataContext.Database.Migrate();
}


app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


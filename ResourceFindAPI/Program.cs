using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using ResourceFindAPI;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Resources") ?? "Data Source=Resources.db";

//builder.Services.AddDbContext<ResourceDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSqlite<ResourceDb>(connectionString);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Yo!");

app.MapGet("/api/resources", async (ResourceDb db) => await db.Resources.ToListAsync());

app.MapPost("/api/resources", async (ResourceDb db, Resource resource) =>
{
    await db.Resources.AddAsync(resource);
    await db.SaveChangesAsync();
    return Results.Created($"/api/resources/{resource.Id}", resource);
});

app.MapGet("/api/resources/{id}", async (ResourceDb db, int id) => 
    await db.Resources.FindAsync(id));

app.Run();
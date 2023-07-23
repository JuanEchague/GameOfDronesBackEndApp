using Microsoft.EntityFrameworkCore;
using GameOfDronesBackEnd.Data;
using GameOfDronesBackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar cadena de conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GameOfDronesContext>(options =>
    options.UseSqlServer(connectionString));


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<PlayerRepository>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();

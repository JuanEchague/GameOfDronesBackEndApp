using Microsoft.EntityFrameworkCore;
using GameOfDronesBackEnd.Data;
using GameOfDronesBackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar cadena de conexi�n a la base de datos
builder.Services.AddDbContext<GameOfDronesContext>(options =>
    options.UseSqlServer(connectionString));


//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("https://juanechague.github.io/GameOfDronesFrontEnd/")
            .AllowAnyOrigin()
            .AllowAnyMethod();
        });

});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<PlayerRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Migrar la base de datos 
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameOfDronesContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseCors(myAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

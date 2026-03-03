using Microsoft.EntityFrameworkCore;
using RoyalGames.Applications.Services;
using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// chamar nossa conexão com o banco aqui na program
builder.Services.AddDbContext<RoyalGamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//Genero
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<GeneroService>();

//Plataforma
builder.Services.AddScoped<IPlataformaRepository, PlataformaRepository>();
builder.Services.AddScoped<PlataformaService>();

//Log
builder.Services.AddScoped<ILogAlteracaoJogoRepository, LogAlteracaoJogoRepository>();
builder.Services.AddScoped<LogAlteracaoJogoService>();


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

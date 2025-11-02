using JBF.Application.Base;
using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using JBF.Application.Services;
using JBF.Domain.Base;
using JBF.Persistence.Base;
using JBF.Persistence.BD;
using JBF.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwagger",
        policy =>
        {
            policy.WithOrigins("http://localhost:5081") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddLogging();

// Repositorios
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();

// Servicios de Aplicación
builder.Services.AddScoped<IServicioService, ServicioService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSwagger");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
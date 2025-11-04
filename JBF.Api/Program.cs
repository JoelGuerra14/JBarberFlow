using JBF.Application.Base;
using JBF.Application.Interfaces;
using JBF.Application.Services;
using JBF.Persistence.Base;
using JBF.Persistence.BD;
using JBF.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:7208", "https://localhost:7208")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:7208",
                    "https://localhost:7208",
                    "http://localhost:5081",
                    "https://localhost:7220"
            )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<ICitasRepository, CitasRepository>();
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<IEstilistasRepository, EstilistaRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();
builder.Services.AddScoped<IDisponibilidadRepository, DisponibilidadRepository>();

builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<IEstilista, EstilistaService>();
builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<IDisponibilidad, DisponibilidadService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.Run();
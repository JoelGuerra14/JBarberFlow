using JBF.Application.Interfaces;
using JBF.Application.Services;
using JBF.Persistence.BD;
using JBF.Persistence.Repositories;

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwagger",
        policy =>
        {
            policy.WithOrigins("http://localhost:7220")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddLogging();

// Repositorios
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<ICitasRepository, CitasRepository>();

// Servicios de Aplicación
builder.Services.AddScoped<ICitaService, CitaService>();


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


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Añadir la conexion con la base de datos cuando se este creando la aplicacion
builder.Services.AddSqlServer<Context>(builder.Configuration.GetConnectionString("AppConnection"));

//Servicios
builder.Services.AddScoped<IEstilista, EstilistaService>();
builder.Services.AddScoped<IEstilistasRepository, EstilistaRepository>();

builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();

builder.Services.AddScoped<IDisponibilidad, DisponibilidadService>();
builder.Services.AddScoped<IDisponibilidadRepository, DisponibilidadRepository>();

/*builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();*/

//PERMITIR CORS PARA QUE EL FETCH PUEDA ACCEDER A LA API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("http://localhost:7220")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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
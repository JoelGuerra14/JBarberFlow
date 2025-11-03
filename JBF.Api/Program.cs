using JBF.Application.Interfaces;
using JBF.Application.Services;
using JBF.Persistence.BD;
using JBF.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

//PERMITIR CORS PARA QUE EL FETCH PUEDA ACCEDER A LA API
app.UseCors("AllowFrontend");

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

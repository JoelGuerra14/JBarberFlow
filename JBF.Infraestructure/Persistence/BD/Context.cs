using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ReservaCitasBackend.Modelos;

namespace Infraestructura.Persistencia.BaseDatos
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContext) : base(dbContext) //Base de datos
        {
        
        }

        //Tablas (primero la entidad y despues el nombre de la tabla en la base de datos)
        public DbSet<MUsers> Usuarios { get; set; }
        public DbSet<MCitas> Citas { get; set; }
        public DbSet<MServicio> Servicios { get; set; }
        public DbSet<MEstilista> Estilistas { get; set; }
        public DbSet<MDisponibilidad> Disponibilidades { get; set; }

    }
}

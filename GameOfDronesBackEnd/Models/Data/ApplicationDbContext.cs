using Microsoft.EntityFrameworkCore;
using GameOfDronesBackEnd.Models;

namespace GameOfDronesBackEnd.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        // Aquí puedes agregar más DbSet para otras entidades si es necesario

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración adicional de las entidades y relaciones, si es necesario
        }
    }
}

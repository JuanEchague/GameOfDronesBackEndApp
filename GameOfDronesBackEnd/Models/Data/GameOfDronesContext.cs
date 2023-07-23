using Microsoft.EntityFrameworkCore;
using GameOfDronesBackEnd.Models;

namespace GameOfDronesBackEnd.Data
{
    public class GameOfDronesContext : DbContext
    {
        public GameOfDronesContext(DbContextOptions<GameOfDronesContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
    }
}
using GameOfDronesBackEnd.Data;
using GameOfDronesBackEnd.Models;

namespace GameOfDronesBackEnd.Repositories
{
    public class PlayerRepository
    {
        private readonly GameOfDronesContext _context;

        public PlayerRepository(GameOfDronesContext context)
        {
            _context = context;
        }

        public Player GetByName(string name)
        {
            return _context.Player.FirstOrDefault(p => p.Name == name);
        }

        public void Add(Player player)
        {
            _context.Player.Add(player);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
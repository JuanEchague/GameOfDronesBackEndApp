using Microsoft.AspNetCore.Mvc;
using GameOfDronesBackEnd.Data;
using GameOfDronesBackEnd.Models;

namespace GameOfDronesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly GameOfDronesContext _context;

        public PlayersController(GameOfDronesContext context)
        {
            _context = context;
        }

        // Mostrar todos los jugadores
        [HttpGet]
        public IActionResult GetPlayer()
        {
            List<Player> player = _context.Player.ToList();
            return Ok(player);
        }

        // Mostrar los detalles de un jugador específico
        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            Player player = _context.Player.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        // Crear un nuevo jugador
        [HttpPost]
        public IActionResult CreatePlayer([FromBody] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Player.Add(player);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
            }
            return BadRequest(ModelState);
        }

        // Actualizar un jugador existente
        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, Player updatedPlayer)
        {
            Player player = _context.Player.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                player.Name = updatedPlayer.Name;
                player.Move = updatedPlayer.Move;
                _context.SaveChanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        // Eliminar un jugador
        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            Player player = _context.Player.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            _context.Player.Remove(player);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
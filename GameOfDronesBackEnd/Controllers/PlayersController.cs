using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        // Acción de lectura para mostrar todos los jugadores
        [HttpGet]
        public IActionResult GetPlayers()
        {
            List<Player> players = _context.Players.ToList();
            return Ok(players);
        }

        // Acción de lectura para mostrar los detalles de un jugador específico
        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            Player player = _context.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        // Acción de escritura para crear un nuevo jugador
        [HttpPost]
        public IActionResult CreatePlayer(Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
            }
            return BadRequest(ModelState);
        }

        // Acción de escritura para actualizar un jugador existente
        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, Player updatedPlayer)
        {
            Player player = _context.Players.Find(id);
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

        // Acción de escritura para eliminar un jugador
        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            Player player = _context.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            _context.Players.Remove(player);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
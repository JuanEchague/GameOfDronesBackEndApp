using Microsoft.AspNetCore.Mvc;
using GameOfDronesBackEnd.Data;
using GameOfDronesBackEnd.Models;
using GameOfDronesBackEnd.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameOfDronesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly GameOfDronesContext _context;
        private readonly PlayerRepository _playerRepository;
        public PlayersController(GameOfDronesContext context)
        {
            _context = context;
            _playerRepository = new PlayerRepository(context);
        }

        // Get all Players
        [HttpGet]
        public IActionResult GetPlayers()
        {
            List<Player> player = _context.Player.ToList();
            return Ok(player);
        }

        // Get player by Id
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
        //Get player by name
        [HttpGet("name/{name}")]
        public ActionResult<Player> GetPlayerByName(string name)
        {
            var player = _playerRepository.GetByName(name);
            if (player == null)
            {
                player = new Player { Name = name, Score = 0 };
                _playerRepository.Add(player);
                _playerRepository.SaveChanges();
            }

            return Ok(player);
        }
        // Create player
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

        // Reset score by id
        [HttpPut("{id}/reset")]
        public IActionResult ResetPlayerScore(int id)
        {
            Player player = _context.Player.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            player.Score = 0; // Establecer el puntaje del jugador a 0
            _context.SaveChanges();

            return NoContent();
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
        //Guardar puntuacion
        [HttpPut("UpdateScore/{id}")]
        public async Task<IActionResult> UpdateScore(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            // Incrementar el puntaje del jugador
            player.Score++;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_playerRepository.PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
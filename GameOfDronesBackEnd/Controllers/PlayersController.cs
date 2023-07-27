using Microsoft.AspNetCore.Mvc;
using GameOfDronesBackEnd.Data;
using GameOfDronesBackEnd.Models;
using GameOfDronesBackEnd.Repositories;

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
        //Buscar jugador por nombre
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
                player.Score = updatedPlayer.Score;
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
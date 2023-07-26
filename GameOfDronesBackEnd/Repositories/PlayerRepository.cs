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

        // Método para mostrar la cantidad de victorias de un jugador
        public int GetPlayerVictories(int playerId)
        {
            return _context.Player.Count(p => p.Id == playerId && p.Move != Move.None);
        }

        // Método para determinar el ganador de una ronda
        public Player DetermineRoundWinner(Player player1, Player player2)
        {
            if (player1.Move == Move.None || player2.Move == Move.None)
                return null;

            if (player1.Move == player2.Move)
                return null;

            if (
                (player1.Move == Move.Rock && player2.Move == Move.Scissors) ||
                (player1.Move == Move.Paper && player2.Move == Move.Rock) ||
                (player1.Move == Move.Scissors && player2.Move == Move.Paper)
            )
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
        // Método para determinar el resultado final de cada partida (tres rondas)
        public Player DetermineGameWinner(Player player1, Player player2)
        {
            int player1Score = GetPlayerVictories(player1.Id);
            int player2Score = GetPlayerVictories(player2.Id);

            if (player1Score > player2Score)
                return player1;
            else if (player2Score > player1Score)
                return player2;
            else
                return null; // Empate
        }

    }
}
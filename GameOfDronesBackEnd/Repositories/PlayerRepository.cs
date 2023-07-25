﻿using System.Collections.Generic;
using System.Linq;
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

        public List<Player> GetAllPlayers()
        {
            return _context.Player.ToList();
        }

        public Player GetPlayerById(int id)
        {
            return _context.Player.FirstOrDefault(p => p.Id == id);
        }

        public void AddPlayer(Player player)
        {
            _context.Player.Add(player);
            _context.SaveChanges();
        }

        // Agrega otros métodos de acuerdo a tus necesidades
    }
}
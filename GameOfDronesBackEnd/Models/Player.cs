using System.ComponentModel.DataAnnotations;

namespace GameOfDronesBackEnd.Models
{
    public class Player
    {
       
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Move Move { get; set; }


                        ///constructores///
        public Player(int id, string name, Move move)
        {
            Id = id;
            Name = name;
            Move = move;
        }
        public Player() : this(0, string.Empty, Move.None)
        {
        }
    }
}

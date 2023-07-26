using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfDronesBackEnd.Models
{
    [Table("Player")]
    public class Player
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Move Move { get; set; }


        ///constructores///

        public Player()
        {
        }
    }
}

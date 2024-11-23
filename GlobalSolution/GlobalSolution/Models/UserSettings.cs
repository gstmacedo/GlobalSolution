using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Models
{
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UsuarioId { get; set; }

        public float LimiteEnergia { get; set; }

        public bool AlertaEmail { get; set; }

        public User User { get; set; }
    }
}

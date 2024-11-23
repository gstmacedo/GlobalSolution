using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UsuarioId { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; }

        [MaxLength(255)]
        public string Descricao { get; set; }

        [MaxLength(255)]
        public string PeriodoDeUso { get; set; }

        public User User { get; set; }
    }
}

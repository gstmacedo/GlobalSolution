using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UsuarioId { get; set; }

        public DateTime PeriodoInicio { get; set; }

        public DateTime PeriodoFim { get; set; }

        public float EnergiaTotal { get; set; }

        public float CustoTotal { get; set; }

        public DateTime DataCriacao { get; set; }

        [MaxLength(255)]
        public string Descricao { get; set; }

        public User User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Models
{
    public class EnergyConsumption
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UsuarioId { get; set; }

        [ForeignKey("Report")]
        public int? RelatorioId { get; set; }

        public DateTime DataCriacao { get; set; }

        public float EnergiaTotal { get; set; }

        public float Custo { get; set; }

        public User User { get; set; }
        public Report Report { get; set; }
    }
}

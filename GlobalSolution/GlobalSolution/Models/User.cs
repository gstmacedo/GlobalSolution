using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; }

        [Required, MaxLength(255), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string SenhaHash { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Models
{
    public class Register
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O nome pode ter no máximo 255 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Password { get; set; }
    }
}

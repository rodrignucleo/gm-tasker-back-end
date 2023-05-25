using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMTasker.API.Models
{
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório!")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório!")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Login é obrigatório!")]
        public string? Login { get; set; }
    }
}
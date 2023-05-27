using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMTasker.API.Models
{
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório!")]
        public string? cpf { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório!")]
        public string? telefone { get; set; }

        [Required(ErrorMessage = "Login é obrigatório!")]
        public string? login { get; set; }

        //[Required(ErrorMessage = "Senha é obrigatório!")]
        [DataType(DataType.Password)]
        public string? senha { get; set; }
    }
}
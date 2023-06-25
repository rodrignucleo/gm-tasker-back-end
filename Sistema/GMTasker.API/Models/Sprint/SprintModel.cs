using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMTasker.API.Models
{
    public class SprintModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_sprint { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? nome { get; set; }

        public string? descricao { get; set; }

        public string?  data_cadastro { get; set; } 
        public string?  data_conclusao { get; set; }
        
        [Required(ErrorMessage = "id_status é obrigatório!")]
        [ForeignKey("Status")]
        public int id_status { get; set; }
        public StatusModel? Status { get; set; }

        [Required(ErrorMessage = "id_usuario_criacao é obrigatório!")]
        [ForeignKey("Usuario")]
        public int? id_usuario_criacao { get; set; }
        public UsuarioModel? Usuario { get; set; }
        
    }
}
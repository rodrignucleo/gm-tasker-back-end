using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMTasker.API.Models
{
    public class RequisicaoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_requisicao { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? nome { get; set; }
        public string? descricao { get; set; }

        [Required(ErrorMessage = "Data é obrigatório!")]
        public string? data_cadastro { get; set; } 
        
        [Required(ErrorMessage = "Data é obrigatório!")]
        public string? data_conclusao { get; set; }
        
        [Required(ErrorMessage = "id_status é obrigatório!")]
        [ForeignKey("Status")]
        public int id_status { get; set; }
        public StatusModel? Status { get; set; }

        //public int id_sprint { get; set; }

        [Required(ErrorMessage = "id_atual_responsavel é obrigatório!")]
        [ForeignKey("UsuarioResponsavel")]
        public int? id_atual_responsavel { get; set; }
        public UsuarioModel? UsuarioResponsavel { get; set; }
        
        [Required(ErrorMessage = "id_usuario_cadastrado é obrigatório!")]
        [ForeignKey("Usuario")]
        public int? id_usuario_criacao { get; set; }
        public UsuarioModel? Usuario { get; set; }

        [ForeignKey("Sprint")]
        public int? id_sprint { get; set; }
        public SprintModel? Sprint { get; set; }
        
    }
}
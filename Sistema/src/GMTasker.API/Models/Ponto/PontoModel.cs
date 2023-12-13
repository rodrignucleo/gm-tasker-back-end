using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMTasker.API.Models
{
    public class PontoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_ponto { get; set; }

        [Required(ErrorMessage = "Data é obrigatório!")]
        public String? data_ponto { get; set; }

        [Required(ErrorMessage = "Hora é obrigatório!")]
        public String? hora_ponto { get; set; }

        [Required(ErrorMessage = "Status é obrigatório!")]
        public String? status { get; set; }
        
        [Required(ErrorMessage = "id_usuario_cadastrado é obrigatório!")]
        [ForeignKey("Usuario")]
        public int? id_usuario_criacao { get; set; }
        public UsuarioModel? Usuario { get; set; }
        
    }
}
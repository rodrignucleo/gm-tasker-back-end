using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMTasker.API.Models
{
    public class StatusModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_status { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? nome { get; set; }

        public bool conta_horas { get; set; }
    }
}
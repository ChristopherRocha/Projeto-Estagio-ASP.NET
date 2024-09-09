using ProjetoEstagio.Classes;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEstagio.Models
{
    public class Fornecedor
    {
        [Key]
        public int id { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string nome { get; set; }
        [MaxLength(14)]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter 14 dígitos.")]
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public string cnpj { get; set; }

        [Required(ErrorMessage = "O segmento é obrigatório.")]
        public Segmento segmento { get; set; }
        [MaxLength(8)]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter 8 dígitos.")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string cep { get; set; }
        [MaxLength(255)]
        public string endereco { get; set; }
        public string? fotoPerfil { get; set; }

    }
   
}

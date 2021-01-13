using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChallengeWebApiJanuary.Models
{
    public class Contrato
    {
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string DataContratacao { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
         public int QuantidadeDeParcelas { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public float ValorFinanciado { get; set; }

        public List<Prestacao> prestacoes { get; set; }
    }
}
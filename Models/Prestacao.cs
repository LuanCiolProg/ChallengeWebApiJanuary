using System;
using System.ComponentModel.DataAnnotations;

namespace ChallengeWebApiJanuary.Models
{

    public class Prestacao
    {
        [Key]
        public int Id { get; set; }

        public Contrato contrato { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataDeVencimento { get; set; }
    
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataDePagamento { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public float Valor { get; set; }

        public string Status { get; set; }
        


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiLanches.Models
{
    public class Ingrediente
    {

        [Key]
        public long IdIngrediente { get; set; }
        [Required(ErrorMessage = "O nome do ingrediente é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Ingrediente")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O valor do ingrediente é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Valor do Ingrediente")]
        public double Valor { get; set; }
        [Display(Name = "Quantidade")]
        public int Qtd { get; set; }
        [Display(Name = "Soma Total")]
        public double SomaTotal { get; set; }
    }
}
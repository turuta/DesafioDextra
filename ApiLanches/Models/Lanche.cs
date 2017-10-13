using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLanches.Models
{
    public class Lanche
    {
        [Key]
        public long IdLanche { get; set; }

        [Required(ErrorMessage = "O nome do lanche é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Lanche")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O valor do lanche é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Valor do Lanche")]
        public double? ValorTotal { get; set; }
        [Display(Name = "Valor de Desconto")]
        public double? Desconto { get; set; }
        [Display(Name = "Lista de Ingredientes")]
        public virtual List<Ingrediente> Ingredientes { get; set; }
    }
}
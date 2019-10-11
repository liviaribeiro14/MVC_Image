using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_Imagem.Models
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage ="O nome do produto é obrigório", AllowEmptyStrings =false)]
        [Display(Name ="Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória", AllowEmptyStrings = false)]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto", AllowEmptyStrings = false)]
        [Display(Name = "Preço")]
        public Decimal Preco { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria", AllowEmptyStrings = false)]
        [Display(Name = "Categoria")]
        public Int32 CategoriaId { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
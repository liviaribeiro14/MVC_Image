using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mvc_Imagem.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public int CategoriaId { get; set; }

        public string CategoriaNome { get; set; }

        public List<Produto> Produtos { get; set; }
    }
}
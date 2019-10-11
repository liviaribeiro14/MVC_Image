using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc_Imagem.Models
{
    public class ProdutoDbContext: DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
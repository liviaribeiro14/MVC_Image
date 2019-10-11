using Mvc_Imagem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Imagem.Controllers
{
    public class ProdutosController : Controller
    {
        ProdutoDbContext db;

        public ProdutosController()
        {
            db = new ProdutoDbContext();
        }
        // GET: Produtos
        public ActionResult Index()
        {
            var produtos = db.Produtos.ToList();
            return View(produtos);
        }
    }
}
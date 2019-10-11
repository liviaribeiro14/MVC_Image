using Mvc_Imagem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public ActionResult Create()
        {
            ViewBag.Categorias = db.Categorias;
            var model = new ProdutoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel model)
        {
            var imageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if(model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "Este campo é obrigatório");
            }
            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Escolha uma imagem GIF, JPG ou PNG");
            }

            if (ModelState.IsValid)
            {
                var produto = new Produto();
                produto.Nome = model.Nome;
                produto.Preco = model.Preco;
                produto.Descricao = model.Descricao;
                produto.CategoriaId = model.CategoriaId;

                var imagemNome = String.Format("{0:yyyyMMdd-HHmmssfff}", DateTime.Now);
                var extensao = System.IO.Path.GetExtension(model.ImageUpload.FileName).ToLower();

                using(var img = System.Drawing.Image.FromStream(model.ImageUpload.InputStream))
                {
                    produto.Imagem = String.Format("/Asserts/{0}{1}", imagemNome, extensao);
                    //Salva imagem
                    SalvarNaPasta(img, produto.Imagem);
                }

                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Se ocorrer um erro, retorna para página
            ViewBag.Categorias = db.Categorias;
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Produto produto = db.Produtos.Find(id);
            if(produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categorias = db.Categorias;
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="ProductId,Nome,Preco,CategoriaId")] Produto model)
        {
            if (ModelState.IsValid)
            {
                var produto = db.Produtos.Find(model.ProdutoId);
                if(produto == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                produto.Nome = model.Nome;
                produto.Preco = model.Preco;
                produto.Descricao = model.Descricao;
                produto.CategoriaId = model.CategoriaId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = db.Categorias;
            return View(model);
        }

        private void SalvarNaPasta(Image img, string caminho)
        {
            using(Image novaImagem = new Bitmap(img))
            {
                novaImagem.Save(Server.MapPath(caminho), img.RawFormat);
            }
        }

    }
}
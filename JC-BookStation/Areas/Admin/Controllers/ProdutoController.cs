using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Aplicacao;
using JC_BookStation.Data.Models;
using PagedList;

namespace JC_BookStation.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: /Produto/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            ViewBag.DateParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var produtos = _db.Produtos.Include(p => p.GrupoProdutos);

            if (!String.IsNullOrEmpty(searchString))
            {
                produtos = produtos.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    produtos = produtos.OrderByDescending(s => s.Nome);
                    break;
                //case "Data":
                //    produtos = produtos.OrderBy(s => s.DataCadastro);
                //    break;
                //case "Data_desc":
                //    produtos = produtos.OrderByDescending(s => s.DataCadastro);
                //    break;
                default:
                    produtos = produtos.OrderBy(s => s.Nome);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(produtos.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Produto/Create
        public ActionResult Create()
        {
            ViewBag.IdGrupo = new SelectList(_db.GrupoProdutos, "IdGrupo", "Nome");
            ViewBag.IdFornecedor = new SelectList(_db.Fornecedores, "idFornecedor", "Nome");
            return View();
        }

        // POST: /Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduto,Nome,Descricao,Nomecupom,ValorVenda,ValorCompra,Lucro,Estoque,ValorPrazo,EstoqueMinimo,Observacao,CodigoBarras,UnCompra,UnVenda,Fator,Comissao,IdGrupo,AtivaSite,NomeFoto")] Produtos produtos)
        {

            var post = Request.Files[0];

            if (post == null)
            {
                ModelState.AddModelError("NomeFoto", "Faltando Imagem do produto ou inválida, tente novamente !!!");
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = new FileInfo(post.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadImages"), file.Name);

                    produtos.NomeFoto = file.Name;
                    _db.Produtos.Add(produtos);
                    _db.SaveChanges();

                    //var obj = new Arquivo();
                    //var idProduto = produtos.IdProduto;

                    //obj.ID = idProduto;
                    //obj.Nome = file.Name;
                    //obj.AnexoExtensao = file.Extension;
                    //obj.AnexoTipo = post.ContentType;
                    post.SaveAs(path);

                    //using (var reader = new BinaryReader(post.InputStream))
                    //    obj.AnexoBytes = reader.ReadBytes(post.ContentLength);
                    //obj.AnexoBytes = null;

                    //_db.Arquivo.Add(obj);
                    //_db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    // retorna o erro se existir algum erro de tipo de dados
                    // no banco de dados, ou no objeto passado para o banco
                    foreach (var error in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entidade do tipo \"{0} \" no estado \"{1} \" tem os seguintes erros de validação:",
                        error.Entry.Entity.GetType().Name, error.Entry.State);
                        foreach (var ve in error.ValidationErrors)
                        {
                            Console.WriteLine("-Propriedade: \"{0}\", Erro: \"{1}\"",
                                                     ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.IdGrupo = new SelectList(_db.GrupoProdutos, "IdGrupo", "Nome", produtos.IdGrupo);
            ViewBag.IdFornecedor = new SelectList(_db.Fornecedores, "idFornecedor", "Nome", produtos.IdFornecedor);
            return View(produtos);
        }

        // GET: /Produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produtos produtos = _db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGrupo = new SelectList(_db.GrupoProdutos, "IdGrupo", "Nome", produtos.IdGrupo);
            ViewBag.IdFornecedor = new SelectList(_db.Fornecedores, "idFornecedor", "Nome", produtos.IdFornecedor);
            Session["Arquivo"] = produtos.NomeFoto ?? "";
            ViewBag.PastaUpload = "/UploadImages/";
            return View(produtos);
        }

        // POST: /Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduto,Nome,Descricao,Nomecupom,ValorVenda,ValorCompra,Lucro,Estoque,ValorPrazo,EstoqueMinimo,Observacao,CodigoBarras,UnCompra,UnVenda,Fator,Comissao,IdGrupo,IdFornecedor,AtivaSite,NomeFoto")] Produtos produtos)
        {
            if (Request.Files.Count != 1)
            {
                ModelState.AddModelError("NomeFoto", "Faltando Imagem do produto ou inválida, tente novamente !!!");
                return View();
            }

            var post = Request.Files[0];

            if (post == null)
            {
                ModelState.AddModelError("NomeFoto", "Imagem do produto inválida, tente novamente !!!");
                return View();
            }

            if (post.FileName != "")
            {
                var arquivoDel = Path.Combine(Server.MapPath("~/UploadImages"), (string)Session["Arquivo"]);

                if (System.IO.File.Exists(@arquivoDel))
                {
                    try
                    {
                        System.IO.File.Delete(@arquivoDel);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                        return View();
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (post.FileName != "")
                    {
                        var file = new FileInfo(post.FileName);
                        var path = Path.Combine(Server.MapPath("~/UploadImages"), file.Name);

                        produtos.NomeFoto = file.Name;
                        post.SaveAs(path);
                    }
                    _db.Entry(produtos).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    // retorna o erro se existir algum erro de tipo de dados
                    // no banco de dados, ou no objeto passado para o banco
                    foreach (var error in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entidade do tipo \"{0} \" no estado \"{1} \" tem os seguintes erros de validação:",
                        error.Entry.Entity.GetType().Name, error.Entry.State);
                        foreach (var ve in error.ValidationErrors)
                        {
                            Console.WriteLine("-Propriedade: \"{0}\", Erro: \"{1}\"",
                                                     ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            ViewBag.IdGrupo = new SelectList(_db.GrupoProdutos, "IdGrupo", "Nome", produtos.IdGrupo);
            ViewBag.IdFornecedor = new SelectList(_db.Fornecedores, "idFornecedor", "Nome", produtos.IdFornecedor);
            return View(produtos);
        }

        [HttpPost]
        public JsonResult DeleteProduto(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                Produtos produtos = _db.Produtos.Find(id);
                _db.Produtos.Remove(produtos);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                mensagemErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return Json(mensagemErro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfEstoque()
        {
            var produtos = (_db.Produtos.Include(p => p.GrupoProdutos).Include(p => p.Fornecedores).OrderBy(p => p.IdGrupo)).ToList();

            return new PdfActionResult(produtos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Data.Models;
using PagedList;

namespace JC_BookStation.Areas.Admin.Controllers
{
    public class LojasController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: Admin/Lojas
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

            var loja = (from func in _db.Loja.OrderBy(l => l.Fantasia)
                        select func);

            if (!String.IsNullOrEmpty(searchString))
            {
                loja = loja.Where(s => s.Fantasia.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    loja = loja.OrderByDescending(s => s.Fantasia);
                    break;
                case "Data":
                    loja = loja.OrderBy(s => s.DataCadastro);
                    break;
                case "Data_desc":
                    loja = loja.OrderByDescending(s => s.DataCadastro);
                    break;
                default:
                    loja = loja.OrderBy(s => s.Fantasia);
                    break;
            }
            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(loja.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Lojas/Create
        public ActionResult Create()
        {
            ViewBag.Estado = _db.Estado.ToList();
            ViewBag.Cidade = _db.Cidade.ToList();
            return View();
        }

        // POST: Admin/Lojas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLoja,CNPJ,CPF,RazaoSocial,Fantasia,NomeContato,CEP,Logradouro,Numero,Bairro,Cidade,Estado,CaminhoLogo,DataCadastro,ATIVO")] Loja loja)
        {
            var post = Request.Files[0];

            if (post == null)
            {
                ModelState.AddModelError("CaminhoLogo", "Faltando Imagem da Logomarca ou inválida, tente novamente !!!");
                return View();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var file = new FileInfo(post.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), file.Name);

                    loja.CaminhoLogo = file.Name;
                    loja.DataCadastro = DateTime.Now;
                    _db.Loja.Add(loja);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0} \" no estado \"{1} \" tem os seguintes erros de validação:",
                    error.Entry.Entity.GetType().Name, error.Entry.State);
                    foreach (var ve in error.ValidationErrors)
                    {
                        Console.WriteLine("-Propriedade: \"{0}\", Erro: \"{1}\"",
                                                 ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            ViewBag.Estado = _db.Estado.ToList();
            ViewBag.Cidade = _db.Cidade.ToList();

            return View(loja);
        }

        // GET: Admin/Lojas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loja loja = _db.Loja.Find(id);
            if (loja == null)
            {
                return HttpNotFound();
            }

            ViewBag.Estado = new SelectList(_db.Estado.OrderBy(d => d.nom_estado).ToList(), "cod_estado", "nom_estado", loja.Estado);
            ViewBag.Cidade = new SelectList(_db.Cidade.Where(d => d.cod_estado == loja.Estado).OrderBy(d => d.cod_cidade).ToList(), "cod_cidade", "nom_cidade", loja.Cidade);
            Session["Arquivo"] = loja.CaminhoLogo ?? "";
            ViewBag.PastaUpload = "/Images/";
            return View(loja);
        }

        // POST: Admin/Lojas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLoja,CNPJ,CPF,RazaoSocial,Fantasia,NomeContato,CEP,Logradouro,Numero,Bairro,Cidade,Estado,CaminhoLogo,DataCadastro,ATIVO")] Loja loja)
        {
            if (Request.Files.Count != 1)
            {
                ModelState.AddModelError("CaminhoLogo", "Faltando Imagem da Logomarca ou inválida, tente novamente !!!");
                return View();
            }

            var post = Request.Files[0];

            if (post == null)
            {
                ModelState.AddModelError("CaminhoLogo", "Imagem do produto inválida, tente novamente !!!");
                return View();
            }

            if (post.FileName != "")
            {
                var arquivoDel = Path.Combine(Server.MapPath("~/Images"), (string)Session["Arquivo"]);

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

            try
            {
                if (ModelState.IsValid)
                {
                    if (post.FileName != "")
                    {
                        var file = new FileInfo(post.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), file.Name);

                        loja.CaminhoLogo = file.Name;
                        post.SaveAs(path);
                    }
                    _db.Entry(loja).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0} \" no estado \"{1} \" tem os seguintes erros de validação:",
                    error.Entry.Entity.GetType().Name, error.Entry.State);
                    foreach (var ve in error.ValidationErrors)
                    {
                        Console.WriteLine("-Propriedade: \"{0}\", Erro: \"{1}\"",
                                                 ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            ViewBag.Estado = new SelectList(_db.Estado.OrderBy(d => d.nom_estado).ToList(), "cod_estado", "nom_estado", loja.Estado);
            ViewBag.Cidade = new SelectList(_db.Cidade.Where(d => d.cod_estado == loja.Estado).OrderBy(d => d.cod_cidade).ToList(), "cod_cidade", "nom_cidade", loja.Cidade);

            return View(loja);
        }

        // POST: Admin/Lojas/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteLoja(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                Loja loja = _db.Loja.Find(id);
                _db.Loja.Remove(loja);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                mensagemErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return Json(mensagemErro, JsonRequestBehavior.DenyGet);
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

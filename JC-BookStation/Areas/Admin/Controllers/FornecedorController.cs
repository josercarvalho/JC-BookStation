using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Data.Models;
using PagedList;

namespace JC_BookStation.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class FornecedorController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: /Fornecedor/
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

            var fornecedores = (from forn in _db.Fornecedores.OrderBy(func => func.Nome)
                                select forn);

            if (!String.IsNullOrEmpty(searchString))
            {
                fornecedores = fornecedores.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    fornecedores = fornecedores.OrderByDescending(s => s.Nome);
                    break;
                //case "Data":
                //    fornecedores = fornecedores.OrderBy(s => s.Email);
                //    break;
                //case "Data_desc":
                //    fornecedores = fornecedores.OrderByDescending(s => s.Email);
                //    break;
                default:
                    fornecedores = fornecedores.OrderBy(s => s.Nome);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(fornecedores.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Fornecedor/Create
        public ActionResult Create()
        {
            ViewBag.Estado = _db.Estado.ToList();
            ViewBag.Cidade = _db.Cidade.ToList();
            return View();
        }

        // POST: /Fornecedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IdFornecedor,Nome,CEP,Endereco,Bairro,Cidade,Estado,CPF,CNPJ,Email,Telefone,Contato,Celular,RGIE")] Fornecedores fornecedores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Fornecedores.Add(fornecedores);
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

            ViewBag.IdFornecedor = new SelectList(_db.Fornecedores, "idFornecedor", "Nome");
            ViewBag.Estado = _db.Estado.ToList();
            ViewBag.Cidade = _db.Cidade.ToList();

            return View(fornecedores);
        }

        // GET: /Fornecedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedores fornecedores = _db.Fornecedores.Find(id);
            if (fornecedores == null)
            {
                return HttpNotFound();
            }

            //ViewBag.Estados = _db.Estado.ToList();
            //ViewBag.Cidades = new SelectList(_db.Cidade, "cod_cidade", "nom_cidade", fornecedores.Cidade);
            CarregaCombo(fornecedores);

            return View(fornecedores);
        }

        // POST: /Fornecedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdFornecedor,Nome,CEP,Endereco,Bairro,Cidade,Estado,CPF,CNPJ,Email,Telefone,Contato,Celular,RGIE")] Fornecedores fornecedores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(fornecedores).State = EntityState.Modified;
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

            //ViewBag.Estados = _db.Estado.ToList();
            //ViewBag.Cidades = new SelectList(_db.Cidade, "cod_cidade", "nom_cidade", fornecedores.Cidade);
            CarregaCombo(fornecedores);

            return View(fornecedores);
        }

        private void CarregaCombo(Fornecedores fornecedores)
        {
            //ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", fornecedores.IdGrupo);
            ViewBag.IdFornecedor = new SelectList(_db.Fornecedores, "idFornecedor", "Nome", fornecedores.IdFornecedor);
            ViewBag.Estado = new SelectList(_db.Estado.OrderBy(d => d.nom_estado).ToList(), "cod_estado", "nom_estado", fornecedores.Estado);
            ViewBag.Cidade = new SelectList(_db.Cidade.Where(d => d.cod_estado == fornecedores.Estado).OrderBy(d => d.cod_cidade).ToList(), "cod_cidade", "nom_cidade", fornecedores.Cidade);
        }

        [HttpPost]
        public JsonResult DeleteFornecedor(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                Fornecedores fornecedores = _db.Fornecedores.Find(id);
                _db.Fornecedores.Remove(fornecedores);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    mensagemErro = ex.InnerException.Message;
                }
                else
                {
                    mensagemErro = ex.Message;
                }
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

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
    public class FuncionarioController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: /Funcionario/
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

            var funcionario = (from func in _db.Funcionarios.OrderBy(func => func.Nome)
                                   select func);

            if (!String.IsNullOrEmpty(searchString))
            {
                funcionario = funcionario.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    funcionario = funcionario.OrderByDescending(s => s.Nome);
                    break;
                case "Data":
                    funcionario = funcionario.OrderBy(s => s.DataDemissao);
                    break;
                case "Data_desc":
                    funcionario = funcionario.OrderByDescending(s => s.DataDemissao);
                    break;
                default:
                    funcionario = funcionario.OrderBy(s => s.Nome);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(funcionario.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Funcionario/Create
        public ActionResult Create()
        {
            ViewBag.Estado = _db.Estado.ToList();
            ViewBag.Cidade = _db.Cidade.ToList();
            return View();
        }

        // POST: /Funcionario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idFuncionario,Nome,CEP,Endereco,Bairro,Cidade,Estado,Nascimento,CPF,RG,Email,Telefone,Celular,DataAdmissao,DataDemissao,Salario,DiaPagamento,Comissao,Observacao")] Funcionarios funcionarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Funcionarios.Add(funcionarios);
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

            return View(funcionarios);
        }

        // GET: /Funcionario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionarios funcionarios = _db.Funcionarios.Find(id);
            if (funcionarios == null)
            {
                return HttpNotFound();
            }

            ViewBag.Estado = new SelectList(_db.Estado.OrderBy(d => d.nom_estado).ToList(), "cod_estado", "nom_estado", funcionarios.Estado);
            ViewBag.Cidade = new SelectList(_db.Cidade.Where(d => d.cod_estado == funcionarios.Estado).OrderBy(d => d.cod_cidade).ToList(), "cod_cidade", "nom_cidade", funcionarios.Cidade);

            return View(funcionarios);
        }

        // POST: /Funcionario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idFuncionario,Nome,CEP,Endereco,Bairro,Cidade,Estado,Nascimento,CPF,RG,Email,Telefone,Celular,DataAdmissao,DataDemissao,Salario,DiaPagamento,Comissao,Observacao")] Funcionarios funcionarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(funcionarios).State = EntityState.Modified;
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

            ViewBag.Estado = new SelectList(_db.Estado.OrderBy(d => d.nom_estado).ToList(), "cod_estado", "nom_estado", funcionarios.Estado);
            ViewBag.Cidade = new SelectList(_db.Cidade.Where(d => d.cod_estado == funcionarios.Estado).OrderBy(d => d.cod_cidade).ToList(), "cod_cidade", "nom_cidade", funcionarios.Cidade);

            return View(funcionarios);
        }

        [HttpPost]
        public JsonResult DeleteFuncionario(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                Funcionarios funcionarios = _db.Funcionarios.Find(id);
                _db.Funcionarios.Remove(funcionarios);
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Models;
using PagedList;

namespace JC_BookStation.Controllers
{
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

            ViewBag.CurrentFilter = searchString;

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

        // GET: /Fornecedor/Details/5
        public ActionResult Details(int? id)
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
            return View(fornecedores);
        }

        // GET: /Fornecedor/Create
        public ActionResult Create()
        {
            ViewBag.Estados = _db.Estado.ToList();
            ViewBag.Cidades = _db.Cidade.ToList();
            return View();
        }

        // POST: /Fornecedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IdFornecedor,Nome,CEP,Endereco,Bairro,Cidade,Estado,CPF,CNPJ,Email,Telefone,Contato,Celular,RGIE")] Fornecedores fornecedores)
        {
            if (ModelState.IsValid)
            {
                _db.Fornecedores.Add(fornecedores);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estados = _db.Estado.ToList();
            ViewBag.Cidades = _db.Cidade.ToList();

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

            ViewBag.Estados = _db.Estado.ToList();
            ViewBag.Cidades = _db.Cidade.Where(cid => cid.cod_estado == fornecedores.Estado).ToList();

            return View(fornecedores);
        }

        // POST: /Fornecedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdFornecedor,Nome,CEP,Endereco,Bairro,Cidade,Estado,CPF,CNPJ,Email,Telefone,Contato,Celular,RGIE")] Fornecedores fornecedores)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(fornecedores).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estados = _db.Estado.ToList();
            ViewBag.Cidades = _db.Cidade.Where(cid => cid.cod_estado == fornecedores.Estado).ToList();

            return View(fornecedores);
        }

        // GET: /Fornecedor/Delete/5
        public ActionResult Delete(int? id)
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

            ViewBag.Estados = _db.Estado.ToList();
            ViewBag.Cidades = _db.Cidade.ToList();

            return View(fornecedores);
        }

        // POST: /Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornecedores fornecedores = _db.Fornecedores.Find(id);
            _db.Fornecedores.Remove(fornecedores);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private IEnumerable<Cidade> GetCidades(int estadoId)
        {
            return _db.Cidade.Where(p => p.cod_estado == estadoId).ToList();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadCidadeId(string estadoId)
        {
            var cidadeList = GetCidades(Convert.ToInt32(estadoId));
            var cidadeData = cidadeList.Select(m => new SelectListItem
            {
                Text = m.nom_cidade,
                Value = m.cod_cidade.ToString(CultureInfo.InvariantCulture),
            });
            return Json(cidadeData, JsonRequestBehavior.AllowGet);
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

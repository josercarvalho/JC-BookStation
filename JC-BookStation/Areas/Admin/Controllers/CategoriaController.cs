using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Data.Models;

namespace JC_BookStation.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: /Categoria/
        public ActionResult Index()
        {
            return View(_db.GrupoProdutos.ToList());
        }
        
        // GET: /Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGrupo,Nome")] GrupoProdutos grupoprodutos)
        {
            if (ModelState.IsValid)
            {
                _db.GrupoProdutos.Add(grupoprodutos);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupoprodutos);
        }

        // GET: /Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoProdutos grupoprodutos = _db.GrupoProdutos.Find(id);
            if (grupoprodutos == null)
            {
                return HttpNotFound();
            }
            return View(grupoprodutos);
        }

        // POST: /Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGrupo,Nome")] GrupoProdutos grupoprodutos)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(grupoprodutos).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupoprodutos);
        }
        
        [HttpPost]
        public JsonResult DeleteCategoria(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                GrupoProdutos grupoprodutos = _db.GrupoProdutos.Find(id);
                _db.GrupoProdutos.Remove(grupoprodutos);
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

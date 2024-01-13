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
    public class GrupoClientesController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: /GrupoClientes/
        public ActionResult Index()
        {
            return View(_db.GrupoClientes.ToList());
        }

        // GET: /GrupoClientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /GrupoClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IdGrupo,Nome")] GrupoClientes grupoclientes)
        {
            if (ModelState.IsValid)
            {
                _db.GrupoClientes.Add(grupoclientes);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupoclientes);
        }

        // GET: /GrupoClientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoClientes grupoclientes = _db.GrupoClientes.Find(id);
            if (grupoclientes == null)
            {
                return HttpNotFound();
            }
            return View(grupoclientes);
        }

        // POST: /GrupoClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdGrupo,Nome")] GrupoClientes grupoclientes)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(grupoclientes).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupoclientes);
        }

        [HttpPost]
        public JsonResult DeleteGrupo(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                GrupoClientes grupoclientes = _db.GrupoClientes.Find(id);
                _db.GrupoClientes.Remove(grupoclientes);
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

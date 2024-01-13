using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using JC_BookStation.Data.Models;
using PagedList;

namespace JC_BookStation.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class BonusController : Controller
    {
        private readonly Entities _db = new Entities();
        // GET: Admin/Venda
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

            var bonus = _db.Comissao.Include(c => c.Clientes).Where(c => c.DataBaixa == null && c.Status == 0);

            if (!String.IsNullOrEmpty(searchString))
            {
                bonus = bonus.Where(c => c.Clientes.Nome.ToUpper().Contains(searchString.ToUpper()) && c.Status == 0);
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    bonus = bonus.OrderByDescending(s => s.Clientes.Nome);
                    break;
                case "Data":
                    bonus = bonus.OrderBy(s => s.DataVenda);
                    break;
                case "Data_desc":
                    bonus = bonus.OrderByDescending(s => s.DataVenda);
                    break;
                default:
                    bonus = bonus.OrderBy(s => s.Clientes.Nome);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(bonus.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BonusPago(string sortOrder, string currentFilter, string searchString, int? page)
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

            var bonus = _db.Comissao.Include(c => c.Clientes).Where(c => c.DataBaixa != null && c.Status == 1);

            if (!String.IsNullOrEmpty(searchString) && searchString != "__/__/____")
            {
                var criterio = (Convert.ToDateTime(searchString)); 
                bonus = bonus.Where(c => c.DataVenda == criterio);
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    bonus = bonus.OrderByDescending(s => s.Clientes.Nome);
                    break;
                case "Data":
                    bonus = bonus.OrderBy(s => s.DataVenda);
                    break;
                case "Data_desc":
                    bonus = bonus.OrderByDescending(s => s.DataVenda);
                    break;
                default:
                    bonus = bonus.OrderByDescending(s => s.DataVenda);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(bonus.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Baixa(int id)
        {
            string mensagemErro = "Pagamento realizado com sucesso...";
            try
            {
                Comissao comissao = _db.Comissao.Find(id);
                comissao.DataBaixa = DateTime.Today.Date;
                comissao.Status = 1;

                _db.Entry(comissao).State = EntityState.Modified;
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

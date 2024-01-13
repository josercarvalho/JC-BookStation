using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Data.Models;

namespace JC_BookStation.Areas.Escritorio.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Entities _db = new Entities();
        // GET: Escritorio/Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Escritorio/Home
        public ActionResult Cliente()
        {
            var id = Session["IdUsuario"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Clientes clientes = _db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
            Session["DataCadastro"] = clientes.DataCadastro;
            CarregaCombo(clientes);
            return View(clientes);
        }
        
        private void CarregaCombo(Clientes clientes)
        {
            ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
            ViewBag.IdIndica = new SelectList(_db.Clientes.OrderBy(c => c.Nome), "idCliente", "NOME", clientes.IdIndica);
            ViewBag.Estado = new SelectList(_db.Estado.OrderBy(d => d.nom_estado).ToList(), "cod_estado", "nom_estado", clientes.Estado);
            ViewBag.Cidade = new SelectList(_db.Cidade.Where(d => d.cod_estado == clientes.Estado).OrderBy(d => d.cod_cidade).ToList(), "cod_cidade", "nom_cidade", clientes.Cidade);
        }

        // GET: Escritorio/Home
        public ActionResult Bonus()
        {
            var bonus = _db.Comissao.ToList();
            return View(bonus);
        }

        // GET: Escritorio/Home
        public ActionResult Rede()
        {
            return View();
        }

        // GET: Escritorio/Home
        public ActionResult Compras()
        {
            return View();
        }

    }
}
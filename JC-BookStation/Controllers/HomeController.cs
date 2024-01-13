using System.Linq;
using System.Web.Mvc;
using JC_BookStation.Aplicacao;
using JC_BookStation.Data.Models;

namespace JC_BookStation.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NossaEmpresa()
        {
            return View();
        }
        public ActionResult Servicos()
        {
            return View();
        }

        public ActionResult Produtos()
        {
            var db = new Entities();

            var produtos = db.Produtos.Where(p => p.AtivaSite == true).OrderBy(p => p.Nome);
            return View(produtos);
        }
        public ActionResult Construcao()
        {
            ViewBag.Message = "Breve em funcionamento.";

            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contato(string nome, string email, string mensagem)
        {
            var envia = new Email();
            var texto = mensagem;
            envia.EnviarEmail("Console Aplication", texto, true, "", new[] { email }, "andreluizjm@hotmail.com");

            return RedirectToAction("Index");
        }

        public ActionResult Suporte()
        {
            ViewBag.Message = "JCarvalho Tecnologia em Informação";

            return View();
        }

        public ActionResult ComoFunciona()
        {
            return View();
        }
    }
}
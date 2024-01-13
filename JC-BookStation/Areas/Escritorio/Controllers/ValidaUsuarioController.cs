using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using JC_BookStation.Data.Models;

namespace JC_BookStation.Areas.Escritorio.Controllers
{
    public class ValidaUsuarioController : Controller
    {
        private static readonly Entities Db = new Entities();

        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        /*************************************************
         *  VALIDAÇÃO DE USUARIO
         * ***********************************************/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AutenticarUsuario(string email, string senha)
        {
            var query = (from u in Db.Clientes
                         where u.Email == email && u.Senha == senha
                         select u).SingleOrDefault();

            //Usuário não existe ou a senha está incorreta
            if (query == null)
            {
                //ViewBag.Erro = "Erro ao fazer login";
                //return false;
                //return RedirectToAction("Index", "Usuario");
                ViewBag.ErrTitulo = "Acesso Restrito.";
                ViewBag.ErrTituloMensagem = "Acesso Negado";
                ViewBag.ErrMensagem = "Verifique Usuário e Senha se estão corretos.";
                return PartialView("_Mensagem");

            }

            //Irá setar um cookie encriptado com o Login do usuário autenticado
            FormsAuthentication.SetAuthCookie(query.Nome, false);

            Session["Usuario"] = query.Nome;
            Session["IdUsuario"] = query.IdCliente;
            Session["SessionID"] = Session.SessionID;

            return RedirectToAction("Index", "Home");
        }

        //[Authorize]
        //public static Usuario GetUsuarioLogado()
        //{
        //    //Pegamos o login do usuário logado
        //    string login = System.Web.HttpContext.Current.User.Identity.Name;

        //    //Naõ existe usuário logado
        //    if (login == "")
        //    {
        //        return null;
        //    }
        //    SalaoEntities entities = new SalaoEntities();

        //    //Buscaos no banco de dados o usuario que está logado
        //    Usuario user = (from u in _db.Usuario
        //        where u.Email == login
        //        select u).SingleOrDefault();

        //    return user;
        //}

        //Deslogando do sistema
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
            ViewBag.Erro = "Usuário desconectado com sucesso";
            return RedirectToAction("Index", "ValidaUsuario");
        }

        public ActionResult RecuperarSenha(string email)
        {

            //var query = _db.Usuario.SingleOrDefault(u => email != null && (email != null && u.Email == email));

            //if (query != null)
            //{
            //    // enviar email para recuperar senha
            //    return Index();
            //}
            //// retorna mensagem de erro, que email nao foi encontrado
            return Index();
        }
    }
}
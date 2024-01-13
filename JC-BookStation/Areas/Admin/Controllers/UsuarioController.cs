using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using JC_BookStation.Data.Models;

namespace JC_BookStation.Areas.Admin.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private static readonly Entities Db = new Entities();

        // GET: /ValidaUsuario/
        [AllowAnonymous]
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        /*************************************************
         *  VALIDAÇÃO DE Usuario
         * ***********************************************/

        [HttpPost]
        [AllowAnonymous]
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

        public static Usuario GetUsuarioLogado()
        {
            //Pegamos o login do usuário logado
            string login = System.Web.HttpContext.Current.User.Identity.Name;

            //Naõ existe usuário logado
            if (login == "")
            {
                return null;
            }
            //Buscaos no banco de dados o Usuario que está logado
            Usuario user = (from u in Db.Usuario
                             where u.Nome == login
                             select u).SingleOrDefault();
            return user;
        }

        //Deslogando do sistema
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
            ViewBag.Erro = "Usuário desconectado com sucesso";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RecuperarSenha(string nomeUser)
        {
            var query = (from u in Db.Usuario
                         where u.Nome == nomeUser
                         select u).SingleOrDefault();

            if (query != null && query.Nome != null)
            {
                // enviar email para recuperar senha
                return Index();
            }
            // retorna mensagem de erro, que email nao foi encontrado
            return Index();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using JC_BookStation.Data.Models;

namespace JC_BookStation.Controllers
{
    public class CadastroController : Controller
    {
        private readonly Entities _db = new Entities();
        //
        // GET: /Cadastro/
        // GET: /Cliente/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome");
            ViewBag.IdIndica = new SelectList(_db.Clientes, "idCliente", "NOME");
            ViewBag.Estados = _db.Estado.ToList();
            ViewBag.Cidades = _db.Cidade.ToList();

            return View();
        }

        // POST: /Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCliente,IdIndica,Nome,Endereco,Bairro,Cidade,Estado,Nascimento,CPF,Email,Senha,Telefone,CEP,Contato,Celular,RG,InscEstadual,Excluido,IdGrupo,DataCadastro,NomeFantasia,LimiteCredito,Observacao,Bloqueado")] Clientes clientes)
        {

            if (IsExistEmail(clientes.Email))
                ModelState.AddModelError("Email", "E-mail cadastrado, tente outro...");

            if (ModelState.IsValid)
            {
                clientes.DataCadastro = DateTime.Now;
                clientes.IdGrupo = 1;
                _db.Clientes.Add(clientes);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            //ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
            CarregaCombo(clientes);
            return View(clientes);
        }
        private void CarregaCombo(Clientes clientes)
        {
            ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
            ViewBag.IdIndica = new SelectList(_db.Clientes, "idCliente", "NOME", clientes.IdIndica);
            ViewBag.Estados = _db.Estado.ToList();
            ViewBag.Cidades = _db.Cidade.Where(cid => cid.cod_estado == clientes.Estado).ToList();
        }

        public JsonResult SearchCliente(string term)
        {
            var clientes = (_db.Clientes.Where(cli => cli.Nome.ToLower().Contains(term.ToLower()))
                .OrderBy(cli => cli.Nome).Select(cli => new { label = cli.Nome }).ToList());

            return Json(clientes, JsonRequestBehavior.AllowGet);
        }
        
        public bool IsExistEmail(string email)
        {
            var login = (_db.Clientes.Find(email));

            if (login != null)
            {
                return false;
            }
            return true;
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
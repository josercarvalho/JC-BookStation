using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Data.Models;
using PagedList;

namespace JC_BookStation.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: /Cliente/
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

            var clientes = _db.Clientes.Include(c => c.GrupoClientes);

            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    clientes = clientes.OrderByDescending(s => s.Nome);
                    break;
                case "Data":
                    clientes = clientes.OrderBy(s => s.DataCadastro);
                    break;
                case "Data_desc":
                    clientes = clientes.OrderByDescending(s => s.DataCadastro);
                    break;
                default:
                    clientes = clientes.OrderBy(s => s.Nome);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(clientes.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Cliente/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome");
            ViewBag.IdIndica = new SelectList(_db.Clientes.OrderBy(c => c.Nome), "idCliente", "NOME");
            ViewBag.Estado = _db.Estado.OrderBy(d => d.nom_estado).ToList();
            //ViewBag.Cidade = _db.Cidade.ToList();

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
                _db.Clientes.Add(clientes);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
            CarregaCombo(clientes);
            return View(clientes);
        }

        // GET: /Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: /Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,IdGrupo,IdIndica,Nome,NomeFantasia,CPF,RG,InscEstadual,Endereco,Bairro,CEP,Cidade,Estado,Nascimento,Email,Senha,Telefone,Celular,Contato,DataCadastro,Observacao,Bloqueado,Excluido")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(clientes).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
            CarregaCombo(clientes);
            return View(clientes);
        }
        //public ActionResult Edit([Bind(Include = "IdCliente,IdIndica,Nome,Endereco,Bairro,Cidade,Estado,Nascimento,CPF,Email,Senha,Telefone,CEP,Contato,Celular,RG,InscEstadual,Excluido,IdGrupo,DataCadastro,NomeFantasia,LimiteCredito,Observacao,Bloqueado")] Clientes clientes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dataCadastro = Convert.ToDateTime(Session["DataCadastro"]);
        //        if (clientes.DataCadastro == null) clientes.DataCadastro = dataCadastro;

        //        _db.Entry(clientes).State = EntityState.Modified;
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
        //    CarregaCombo(clientes);
        //    return View(clientes);
        //}

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

        //public JsonResult LoadCidadeId(string estadoId)
        //{
        //    estadoId
        //    return Json(_db.Cidade.Where(d => d.cod_estado == estadoId).Select(d => new { id = d.cod_cidade, nome = d.nom_cidade }), JsonRequestBehavior.AllowGet);
        //}

        private void CarregaCombo(Clientes clientes)
        {
            ViewBag.IdGrupo = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", clientes.IdGrupo);
            ViewBag.IdIndica = new SelectList(_db.Clientes.OrderBy(c => c.Nome), "idCliente", "NOME", clientes.IdIndica);
            ViewBag.Estado = new SelectList(_db.Estado.OrderBy(d => d.nom_estado).ToList(), "cod_estado", "nom_estado", clientes.Estado);
            ViewBag.Cidade = new SelectList(_db.Cidade.Where(d => d.cod_estado == clientes.Estado).OrderBy(d => d.cod_cidade).ToList(), "cod_cidade", "nom_cidade", clientes.Cidade);
        }
        public bool IsExistEmail(string email)
        {
            var query = (_db.Clientes.Where(u => u.Email == email)).FirstOrDefault();

            return query != null;
        }

        public PartialViewResult _Indica(int id, string nome)
        {
            var indicados = (_db.Clientes.Where(cli => cli.IdIndica == nome)).ToList();
            ViewBag.ClientesIndicados = indicados;
            ViewBag.IndicadosTotal = indicados.Count();
            ViewBag.Indicante = nome;
            System.Threading.Thread.Sleep(2000);
            return PartialView(indicados);
        }

        public JsonResult SearchCliente(string term)
        {
            var clientes = (_db.Clientes.Where(cli => cli.Nome.ToLower().Contains(term.ToLower()))
                .OrderBy(cli => cli.Nome).Select(cli => new { label = cli.Nome}).ToList());

            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCliente(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                Clientes clientes = _db.Clientes.Find(id);
                _db.Clientes.Remove(clientes);
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

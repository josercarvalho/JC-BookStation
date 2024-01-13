using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Data.Models;
using PagedList;

namespace JC_BookStation.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class CompraController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: /Produto/
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

            var compras = _db.Compra.OrderByDescending(s => s.DataCompra);

            if (!String.IsNullOrEmpty(searchString))
            {
                compras = (IOrderedQueryable<Compra>)compras.Where(s => s.Fornecedores.Nome.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    compras = compras.OrderBy(s => s.Fornecedores.Nome);
                    break;
                //case "Data":
                //    compras = compras.OrderBy(s => s.Fornecedores.Nome);
                //    break;
                case "Data_desc":
                    compras = compras.OrderBy(s => s.DataCompra);
                    break;
                default:
                    compras = compras.OrderByDescending(s => s.DataCompra);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(compras.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.IdFornecedor = new SelectList(_db.Fornecedores, "IdFornecedor", "Nome");
            ViewBag.IdFinanceiroTipo = new SelectList(_db.FinanceiroTipo, "IdFinanceiroTipo", "FinanceiroTipoDesc");

            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult BuscaNotaFiscal(string codigoNota)
        {
            //var id = Convert.ToInt32(Session["IdLoja"]);
            var retorno = "1";

            var compraBusca = (_db.Compra.Where(u => u.CodigoNota == codigoNota)
                .Select(u => new { u.CodigoNota })).FirstOrDefault();

            if (compraBusca != null) retorno = compraBusca.ToString();

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadProdutoId(Int64 produtoId)
        {
            //var id = Convert.ToInt32(Session["IdLoja"]);
            //var id = Convert.ToInt32(produtoId);

            var produtoBusca = (_db.Produtos.Where(u => u.CodigoBarras == produtoId)
                .Select(u => new { u.IdProduto, u.Nome, u.ValorCompra })).FirstOrDefault();

            return Json(produtoBusca, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public HttpStatusCodeResult SalvarProdutos(Compra compra, Financeiro financeiro, ProdutosCompra[] produtosCompra)
        {
            try
            {
                //var idLoja = Convert.ToInt32(Session["IdLoja"]);

                //compra.IdLoja = idLoja;
                compra.ValorTotal = compra.ValorTotal;
                compra.Parcelas = financeiro.Parcelas;
                compra.Obs = financeiro.Obs;
                _db.Compra.Add(compra);
                _db.SaveChanges();

                if (financeiro.Parcelas == 1)
                {
                    financeiro.IdCompra = compra.IdCompra;
                    //financeiro.IdLoja = Convert.ToInt32(idLoja);
                    financeiro.Valor = compra.ValorTotal;
                    financeiro.Vencimento = compra.DataCompra;
                    _db.Financeiro.Add(financeiro);
                }
                else
                {
                    for (var i = 0; i < compra.Parcelas; i++)
                    {
                        financeiro.IdCompra = compra.IdCompra;
                        //financeiro.IdLoja = Convert.ToInt32(idLoja);
                        financeiro.Parcelas = Convert.ToInt32(i);
                        var valorParcela = (compra.ValorTotal / compra.Parcelas);
                        financeiro.Valor = valorParcela;
                        if (i > 1)
                        {
                            var meses = (i - 1);
                            var novaData = Convert.ToDateTime(financeiro.Vencimento).AddMonths(meses);
                            financeiro.Vencimento = novaData;
                        }
                        _db.Financeiro.Add(financeiro);
                    }
                }

                _db.SaveChanges();

                foreach (var produto in produtosCompra)
                {
                    //Busca produto para atualização do item da compra, atualizar o estoque pelo codigo de barras da compra
                    var idProduto = Convert.ToInt64(produto.IdProduto);
                    var produtoBusca = (_db.Produtos.Where(u => u.IdProduto == idProduto)).FirstOrDefault();

                    //Atualiza os campos dos itens da compra a serem salvos
                    produto.IdCompra = compra.IdCompra;
                    //produto.IdLoja = idLoja;
                    if (produtoBusca != null)
                    {
                        produto.IdProduto = produtoBusca.IdProduto;
                        produto.Quantidade = produto.Quantidade;

                        //Salva itens da compra
                        _db.ProdutosCompra.Add(produto);
                        _db.SaveChanges();

                        //Atualiza o Estoque do produto
                        produtoBusca.Estoque += produto.Quantidade;
                        produtoBusca.ValorCompra = produto.ValorUnitario;
                        _db.Entry(produtoBusca).State = EntityState.Modified;
                    }
                    _db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var error in e.EntityValidationErrors)
                {
                    Console.WriteLine(
                        "Entidade do tipo \"{0} \" no estado \"{1} \" tem os seguintes erros de validação:",
                        error.Entry.Entity.GetType().Name, error.Entry.State);
                    foreach (var ve in error.ValidationErrors)
                    {
                        Console.WriteLine("-Propriedade: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public JsonResult DeleteCompra(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                var produtosCompras = _db.ProdutosCompra.Where(c => c.IdCompra == id);
                _db.ProdutosCompra.RemoveRange(produtosCompras);

                var financeiro = _db.Financeiro.Where(f => f.IdCompra == id);
                _db.Financeiro.RemoveRange(financeiro);

                var compra = _db.Compra.Find(id);
                _db.Compra.Remove(compra);

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                mensagemErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return Json(mensagemErro, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
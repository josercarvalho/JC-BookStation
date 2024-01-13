using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JC_BookStation.Aplicacao;
using JC_BookStation.Data.Models;
using JC_BookStation.ViewModels;
using PagedList;

namespace JC_BookStation.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class VendaController : Controller
    {
        private readonly Entities _db = new Entities();

        #region Listagens das vendas
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

        public ActionResult VendaDireta(string sortOrder, string currentFilter, string searchString, int? page)
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

            var vendas = _db.Venda.Where(v => v.TipoVenda == 0 && v.IdFinanceiroTipo < 5).Include(v => v.Financeiro).Include(v => v.Clientes);

            if (!String.IsNullOrEmpty(searchString))
            {
                var criterio = (Convert.ToDateTime(searchString));
                vendas = vendas.Where(v => v.DataVenda == criterio);
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    vendas = vendas.OrderByDescending(s => s.DataVenda);
                    break;
                case "Data":
                    vendas = vendas.OrderBy(s => s.DataVenda);
                    break;
                case "Data_desc":
                    vendas = vendas.OrderByDescending(s => s.DataVenda);
                    break;
                default:
                    vendas = vendas.OrderBy(s => s.DataVenda);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(vendas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Consignadas(string sortOrder, string currentFilter, string searchString, int? page)
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

            var vendas = _db.Venda.Where(v => v.TipoVenda == 1 && v.IdFinanceiroTipo == 5).Include(v => v.Financeiro).Include(v => v.Clientes);

            if (!String.IsNullOrEmpty(searchString))
            {
                var criterio = (Convert.ToDateTime(searchString));
                vendas = vendas.Where(v => v.DataVenda == criterio);
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    vendas = vendas.OrderByDescending(s => s.DataVenda);
                    break;
                case "Data":
                    vendas = vendas.OrderBy(s => s.DataVenda);
                    break;
                case "Data_desc":
                    vendas = vendas.OrderByDescending(s => s.DataVenda);
                    break;
                default:
                    vendas = vendas.OrderBy(s => s.DataVenda);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(vendas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult VendaBaixa(string sortOrder, string currentFilter, string searchString, int? page)
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

            var vendas = _db.Venda.Where(v => v.TipoVenda == 1 && v.StatusVenda == "A").Include(v => v.Financeiro).Include(v => v.Clientes);

            if (!String.IsNullOrEmpty(searchString))
            {
                var criterio = (Convert.ToDateTime(searchString));
                vendas = vendas.Where(v => v.DataVenda == criterio);
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    vendas = vendas.OrderByDescending(s => s.DataVenda);
                    break;
                case "Data":
                    vendas = vendas.OrderBy(s => s.DataVenda);
                    break;
                case "Data_desc":
                    vendas = vendas.OrderByDescending(s => s.DataVenda);
                    break;
                default:
                    vendas = vendas.OrderBy(s => s.DataVenda);
                    break;
            }

            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(vendas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MovimentoVenda(string sortOrder, string currentFilter, string strDataInicial, string strDataFinal, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            ViewBag.DateParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (strDataInicial != null)
            {
                page = 1;
            }
            else
            {
                strDataInicial = currentFilter;
            }

            ViewBag.CurrentFilter = strDataInicial;

            var vendas = _db.Venda.Include(v => v.ProdutosVenda).OrderByDescending(v => v.DataVenda).ToList();
            string periodo;

            if (!string.IsNullOrEmpty(strDataInicial))
            {
                var dataIni = Convert.ToDateTime(strDataInicial);
                var dataFin = Convert.ToDateTime(strDataFinal);

                periodo = "Período de " + strDataInicial + " a " + strDataFinal;
                vendas = (vendas.Where(v => v.DataVenda >= dataIni && v.DataVenda <= dataFin)).ToList();
            }
            else
            {
                var culture = new CultureInfo("pt-BR");
                var formataData = culture.DateTimeFormat;
                var mes = Convert.ToInt32(DateTime.Now.Month);
                var nomeMes = culture.TextInfo.ToTitleCase(formataData.GetMonthName(mes));

                periodo = "Período do mês de " + nomeMes;
                vendas = vendas.Where(v => v.DataVenda != null && v.DataVenda.Value.Month.Equals(mes)).ToList();
            }
            ViewBag.Periodo = periodo;

            //var model = new VendaMovViewModel
            //{
            //    TotalVendas = vendas.Count(),
            //    Periodo = periodo,
            //    ValorTotal = vendas.Sum(v => v.Valor),
            //    Vendas = vendas
            //};

            const int pageSize = 5;
            var pageNumber = (page ?? 1);
            return View(vendas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LucroVenda(string strDataInicial, string strDataFinal)
        {
            string periodo;
            var vendas = _db.Venda.Include(v => v.Comissao).ToList();
            var comissao = _db.Comissao.ToList();

            if (!string.IsNullOrEmpty(strDataInicial))
            {
                var dataIni = Convert.ToDateTime(strDataInicial);
                var dataFin = Convert.ToDateTime(strDataFinal);

                periodo = "de " + strDataInicial + " a " + strDataFinal;
                vendas = (vendas.Where(v => v.DataVenda >= dataIni && v.DataVenda <= dataFin)).ToList();
                comissao = (comissao.Where(c => c.DataBaixa >= dataIni && c.DataVenda <= dataFin)).ToList();
            }
            else
            {
                var culture = new CultureInfo("pt-BR");
                var formataData = culture.DateTimeFormat;
                var mes = Convert.ToInt32(DateTime.Now.Month);
                var nomeMes = culture.TextInfo.ToTitleCase(formataData.GetMonthName(mes));

                periodo = "do mês de " + nomeMes;
                vendas = (vendas.Where(v => v.DataVenda != null && v.DataVenda.Value.Month.Equals(mes))).ToList();
                comissao = (comissao.Where(c => c.DataBaixa != null && c.DataVenda.Value.Month.Equals(mes))).ToList();
            }

            ViewBag.Periodo = periodo;
            var totalComissao = comissao.Sum(c => c.ValorComissao);
            var valorTotal = vendas.Sum(v => v.Valor);

            foreach (var venda in vendas)
            {
                Lucro += CalculaLucro(venda.IdVenda);
            }

            var valorLucro = (Lucro - totalComissao);

            var model = new LucrosViewModel
            {
                TotalVendas = vendas.Count(),
                Periodo = periodo,
                ValorBonus = (decimal) totalComissao,
                ValorTotal = valorTotal,
                ValorLucro = (decimal) valorLucro
            };

            return View(model);
        }

        private decimal CalculaLucro(int id)
        {
            var produtosVenda = (_db.ProdutosVenda.Where(v => v.IdVenda == id));

            foreach (var produto in produtosVenda)
            {
                var valorCompraProduto = (_db.Produtos.Find(produto.IdProduto));

                ProdVal += (valorCompraProduto.ValorCompra*produto.Quantidade);
            }

            if (ProdVal == null)
            {
                ProdVal = 1;
            }

            return (decimal) ProdVal;
        }

        public PartialViewResult _ListarVenda(int id, string nome)
        {
            var venda = _db.Venda.Where(v => v.IdVenda == id).Include(v => v.ProdutosVenda);
            ViewBag.Nome = nome;
            System.Threading.Thread.Sleep(2000);
            return PartialView(venda);
        }

        #endregion

        #region Movimentos do Estoque
        //
        // GET: /Venda/Create
        // passa o ID do cliente
        public ActionResult Create(int id = 0)
        {
            var venda = new Venda
            {
                IdCliente = id < 1 ? 1 : id,
                DataVenda = DateTime.Today
            };

            Session["idCliente"] = id;

            ViewBag.IdFinanceiroTipo = new SelectList(_db.FinanceiroTipo, "IdFinanceiroTipo", "FinanceiroTipoDesc");
            ViewBag.IdCliente = (_db.Clientes.Where(c => c.IdCliente == id).Select(c => c.Nome));
            ViewBag.DataVenda = DateTime.Now;

            return View(venda);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult BuscaProduto(Int64 codigoBarras)
        {
            //var id = Convert.ToInt64(Session["IdLoja"]);

            var produtoBusca = (_db.Produtos.Where(u => u.CodigoBarras == codigoBarras)
                .Select(u => new { u.IdProduto, u.Nome, u.ValorVenda, u.Comissao, u.Estoque })).FirstOrDefault();

            return Json(produtoBusca, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public HttpStatusCodeResult SalvarProdutos(Venda venda, Financeiro financeiro, ProdutosVenda[] produtosVenda)
        {
            try
            {
                var dataVenda = DateTime.Now.Date;
                var idCli = Convert.ToInt32(Session["idCliente"]);
                var valorVenda = produtosVenda.Sum(p => p.SubTotal);

                var novaVenda = new Venda
                {
                    IdCliente = idCli,
                    IdFinanceiroTipo = financeiro.IdFinanceiroTipo,
                    TipoVenda = venda.TipoVenda,
                    Parcelas = financeiro.Parcelas,
                    DataVenda = dataVenda,
                    Valor = (decimal)valorVenda,
                    StatusVenda = venda.IdFinanceiroTipo == 0 ? "F" : "A",
                    Obs = financeiro.Obs
                };

                _db.Venda.Add(novaVenda);
                _db.SaveChanges();
                Session["IdVenda"] = novaVenda.IdVenda;

                if (financeiro.Parcelas == 1)
                {
                    financeiro.IdVenda = novaVenda.IdVenda;
                    financeiro.Valor = novaVenda.Valor;
                    financeiro.Vencimento = novaVenda.DataVenda;
                    _db.Financeiro.Add(financeiro);
                }
                else
                {
                    for (var i = 0; i < financeiro.Parcelas; i++)
                    {
                        financeiro.IdVenda = novaVenda.IdVenda;
                        financeiro.Parcelas = Convert.ToInt32(i);
                        var valorParcela = (novaVenda.Valor / financeiro.Parcelas);
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

                var bonus = (decimal)0.00;

                foreach (var produto in produtosVenda)
                {
                    //Busca produto para atualização do item da venda, atualizar o estoque pelo codigo de barras da venda
                    var idProduto = Convert.ToInt64(produto.IdProduto);
                    var produtoBusca = (_db.Produtos.FirstOrDefault(u => u.IdProduto == idProduto));

                    if (produtoBusca != null)
                    {
                        //Atualiza os campos dos itens da venda a serem salvos
                        produto.IdVenda = novaVenda.IdVenda;
                        produto.IdProduto = produtoBusca.IdProduto;
                        produto.Quantidade = produto.Quantidade;
                        produto.Bonus = (produto.Quantidade * produtoBusca.Comissao);
                        produto.SubTotal = (produto.Quantidade * produtoBusca.ValorVenda);

                        //Salva itens da venda
                        _db.ProdutosVenda.Add(produto);
                        _db.SaveChanges();

                        //Atualiza o Estoque do produto
                        produtoBusca.Estoque -= produto.Quantidade;
                        _db.Entry(produtoBusca).State = EntityState.Modified;
                        _db.SaveChanges();

                        //Calcula Bonus do Colaborador
                        bonus += (decimal)(produtoBusca.Comissao * produto.Quantidade);
                    }
                }

                if (novaVenda.StatusVenda == "F")
                {
                    //dataVenda = (DateTime)novaVenda.DataVenda;
                    GerarBonus(novaVenda.IdCliente, novaVenda.IdVenda, dataVenda, bonus);
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
        //[ValidateAntiForgeryToken]
        public JsonResult DevolveProduto(int id, int quantidade)
        {
            var mensagemErro = "Devolvido ao ESTOUE com sucesso...";
            try
            {
                //Busca produto da Venda e produto para atualizar o estoue
                ProdutosVenda produtosVenda = (_db.ProdutosVenda.Find(id));
                var produtoBusca = (_db.Produtos.FirstOrDefault(u => u.IdProduto == produtosVenda.IdProduto));

                //Atualiza o Estoque do produto
                if (produtoBusca != null)
                {
                    produtoBusca.Estoque += quantidade;
                    _db.Entry(produtoBusca).State = EntityState.Modified;
                }
                //_db.SaveChanges();

                //Atualiza ou Remove o produto da lista de vendas
                produtosVenda.Quantidade = (produtosVenda.Quantidade - quantidade);
                produtosVenda.Bonus = (produtosVenda.Quantidade * produtoBusca.Comissao);
                produtosVenda.SubTotal = (produtosVenda.ValorUnitario * produtosVenda.Quantidade);
                _db.Entry(produtosVenda).State = EntityState.Modified;

                //Atualiza o valor da Venda sem os produtos devolvidos
                Venda venda = (_db.Venda.Find(produtosVenda.IdVenda));

                venda.Valor = (decimal)(venda.Valor - (produtoBusca.ValorVenda * quantidade));
                _db.Entry(produtoBusca).State = EntityState.Modified;
                //_db.SaveChanges();

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                mensagemErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            System.Threading.Thread.Sleep(1500);
            return Json(mensagemErro, JsonRequestBehavior.AllowGet);
        }

        #endregion

        private void GerarBonus(int id, int idVenda, DateTime dataVenda, decimal bonus)
        {
            var cliente = (_db.Clientes.Find(id));

            //Atualiza Bonus do Indicador
            if (cliente.IdIndica != null)
            {
                var idCliente = (_db.Clientes.Where(c => c.Nome == cliente.IdIndica).Select(c => c.IdCliente)).FirstOrDefault();

                var indicante = new Comissao
                {
                    idCliente = Convert.ToInt32(idCliente),
                    idVenda = idVenda,
                    DataVenda = dataVenda,
                    ValorComissao = bonus,
                    Status = 0
                };
                _db.Comissao.Add(indicante);
                _db.SaveChanges();
            }
        }

        public ActionResult BaixaConsignada(int id)
        {
            Session["IdVenda"] = id;
            var vendas = (_db.Venda.Find(id));
            var cliente = (_db.Clientes.Find(vendas.IdCliente));
            var produtos = (_db.ProdutosVenda.Where(v => v.IdVenda == id)).ToList();

            var comissionada = new VendaBaixaConsignadaViewModel
            {
                IdVenda = id,
                Nome = cliente.Nome,
                TipoVenda = "CONSIGNADA",
                FormaPagamento = "PROMISSÓRIA",
                Parcelas = vendas.Parcelas ?? 1,
                DataVenda = (DateTime)vendas.DataVenda,
                Valor = vendas.Valor,
                ProdutosVendas = produtos.ToList()
            };
            return View(comissionada);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult FinalizarVenda(int id)
        {
            var mensagemErro = "VENDA FINALIZADA com sucesso...";

            try
            {
                var venda = _db.Venda.Find(id);
                venda.StatusVenda = "F";
                _db.Entry(venda).State = EntityState.Modified;
                _db.SaveChanges();

                var buscaBonus = (_db.ProdutosVenda.Where(p => p.IdVenda == id));

                var bonus = (decimal)buscaBonus.Sum(p => p.Bonus);
                var dataVenda = (DateTime)venda.DataVenda;

                GerarBonus(venda.IdCliente, venda.IdVenda, dataVenda, bonus);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            System.Threading.Thread.Sleep(1500);
            return Json(mensagemErro, JsonRequestBehavior.AllowGet);
        }
        
        public PartialViewResult _Vendas(int id, string nome)
        {
            var vendasCliente = (_db.Venda.Where(cli => cli.IdCliente == id).Include(cli => cli.Financeiro)).ToList();

            ViewBag.VendasTotal = vendasCliente.Count();
            ViewBag.Cliente = nome;
            // ViewBag.TipoVenda = new SelectList(_db.GrupoClientes, "IdGrupo", "Nome", vendasCliente.Tipo);
            System.Threading.Thread.Sleep(2000);

            return PartialView(vendasCliente);
        }

        [HttpPost]
        public JsonResult DeleteVenda(int id)
        {
            var mensagemErro = "Excluído com sucesso...";
            try
            {
                var produtosVenda = (_db.ProdutosVenda.Where(p => p.IdVenda == id));

                //Devolve os produtos ao estoque
                foreach (var produto in produtosVenda)
                {
                    var idProduto = Convert.ToInt64(produto.IdProduto);
                    var produtoBusca = (_db.Produtos.FirstOrDefault(u => u.IdProduto == idProduto));

                    //Atualiza o Estoque do produto
                    if (produtoBusca != null)
                    {
                        produtoBusca.Estoque += produto.Quantidade;
                        _db.Entry(produtoBusca).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }

                _db.ProdutosVenda.RemoveRange(produtosVenda);

                _db.Financeiro.RemoveRange(_db.Financeiro.Where(f => f.IdVenda == id));

                var venda = _db.Venda.Find(id);
                _db.Venda.Remove(venda);

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                mensagemErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return Json(mensagemErro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PdfNotaPromissoria(int id, string vencimento)
        {
            var vendas = (_db.Venda.Find(id));
            var cliente = (_db.Clientes.Find(vendas.IdCliente));
            var loja = (_db.Loja.First());
            var endLoja = loja.Logradouro + ", " + loja.Numero + ". " + loja.Bairro + ", " + loja.Cidade + " - " +
                          loja.Estado + ".";
            //var financeiro = _db.Financeiro.Where(f => f.IdFinanceiro == vendas.IdFinanceiroTipo);

            var valorExtenso = Converter.ToExtenso(vendas.Valor);
            var promissoria = new NotaPromissoriaViewModel
            {
                IdNota = id,
                Nome = cliente.Nome,
                Valor = vendas.Valor,
                ValorExtenso = valorExtenso,
                CPF = cliente.CEP,
                Vencimento = Convert.ToDateTime(vencimento),
                NomeEmpresa = loja.Fantasia,
                CNPJEmpresa = loja.CNPJ,
                EnderecoEmpresa = endLoja

            };

            return new PdfActionResult(promissoria);
        }

        //[HttpPost]
        public ActionResult PdfProdutosVenda()
        {
            var id = Convert.ToInt32(Session["IdVenda"]);
            var venda = _db.Venda.First(v => v.IdVenda == id);

            var produtos = (_db.ProdutosVenda.Where(v => v.IdVenda == id)).ToList();
            //DateTime dataExtenso = Converter.DataExtenso(DateTime.Now);

            var model = new VendaViewModel
            {
                Venda = venda,
                ValorTotal = venda.Valor,
                Nome = venda.Clientes.Nome,
                DataVenda = venda.DataVenda,
                TotalItens = produtos.Sum(p => p.Quantidade),
                //TotalBonus = (produtos * produtos.Sum(p => p.Quantidade)),
                SubTotal = produtos.Sum(p => p.SubTotal),
                ProdutosVendas = produtos.ToList()
            };
            return new PdfActionResult(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public decimal Lucro { get; set; }

        public decimal? ProdVal { get; set; }
    }
}
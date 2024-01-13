using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC_BookStation.Data.MetaData
{
    internal class ProdutosOrcamentoMetadata
    {
        public int Id { get; set; }
        public int IdOrcamento { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public int? Quantidade { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
    }
}

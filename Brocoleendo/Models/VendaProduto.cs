namespace Brocoleendo.Models
{
    public class VendaProduto
    {
        public int ID_VENDA_PRODUTO { get; set; }
        public int ID_PRODUTO { get; set; }
        public int ID_VENDA { get; set; }
        public int ID_Estoque_Prod { get; set; }
        public string FORMA_PGTO { get; set; }
        public DateTime DT_VENDA { get; set; }
        public string NOME_FUNC { get; set; }
        public string DS_PRODUTO { get; set; }
        public string Url_Img { get; set; }
        public decimal KG_PRODUTO { get; set; }
        public decimal PRECO_UNI { get; set; }
        public decimal PRECO_TOT { get; set; }
    }
}

namespace Brocoleendo.Models
{
    public class Venda
    {
        public int ID_VENDA { get; set; }
        public string FORMA_PGTO { get; set; }
        public DateTime DT_VENDA { get; set; }
        public int ID_FUNC { get; set; }
        public string NOME_FUNC { get; set; }
        public decimal PRECO_TOT { get; set; }
        public bool ST_ATIVO { get; set; }
    }
}

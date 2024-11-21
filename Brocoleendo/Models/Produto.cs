namespace Brocoleendo.Models
{
    public class Produto
    {
        public int ID_PRODUTO { get; set; }
        public string DS_PRODUTO { get; set; }
        public string Url_Img { get; set; }
        public double PRECO { get; set; }
        public double SALDO { get; set; }
        public string DS_GRUPO { get; set; }
        public int ID_GRUPO { get; set; }
        public bool ST_Ativo = true;



    }
}

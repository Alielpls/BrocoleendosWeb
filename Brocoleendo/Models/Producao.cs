namespace Brocoleendo.Models
{
    public class Producao
    {
        public int ID_PRODUCAO { get; set; }
        public DateTime DT_PLANTIO { get; set; }
        public DateTime DT_ESTIMADA { get; set; }
        public DateTime DT_COLHEITA { get; set; }
        public int ID_LOCALIZACAO_LOTE { get; set; }
        public bool ST_ATIVO { get; set; }
        public int ID_Produto { get; set; }
        public decimal KG_RENDIDO { get; set; }
    }
}

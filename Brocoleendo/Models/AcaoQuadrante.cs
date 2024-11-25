namespace Brocoleendo.Model
{
    public class AcaoQuadrante
    {
        public int ID_PRODUCAO { get; set; }
        public int ID_ACAO_PRODUCAO { get; set; }
        public string DS_ACAO { get; set; }
        public DateTime DT_ACAO { get; set; }
        public string NOME_FUNC { get; set; }
        public int ID_FUNC { get; set; }
        public string DS_MATERIAL { get; set; }
        public int ID_MATERIAL { get; set; }
        public decimal KG_MATERIAL { get; set; }
        public string DS_QUADRANTE { get; set; }
        public int ID_LOCALIZACAO_LOTE { get; set; }
        public bool ST_ATIVO { get; set; }

    }
}

namespace Brocoleendo.Models
{
    public class Producao
    {
        public int ID_Localizacao_Lote { get; set; }
        public string DS_Quadrante { get; set; }
        public bool ST_USO { get; set; }
        public int ID_Producao { get; set; }
        public bool ST_Ativo { get; set; }

        public Producao(int ID_Localizacao, string DS_Quadrante, bool ST_Uso, int ID_Producao, bool ST_Ativo)
        {
            this.ID_Localizacao_Lote = ID_Localizacao;
            this.DS_Quadrante = DS_Quadrante;
            this.ST_USO = ST_Uso;
            this.ID_Producao = ID_Producao;
            this.ST_Ativo = ST_Ativo;
        }


    }
}

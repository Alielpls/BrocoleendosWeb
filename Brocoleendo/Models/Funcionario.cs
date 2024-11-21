namespace Brocoleendo.Models
{
    public class Funcionario
    {
        public int ID_FUNC { get; set; }
        public string NOME_FUNC { get; set; }
        public string TELEFONE { get; set; }
        public string SENHA_CRT { get; set; }
        public DateTime DT_REGISTRO { get; set; }
        public bool ST_ATIVO { get; set; }

    }
}

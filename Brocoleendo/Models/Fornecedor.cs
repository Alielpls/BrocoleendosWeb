using System.ComponentModel;

namespace Brocoleendo.Models
{
    public class Fornecedor
    {
        [DisplayName("ID")]
        public int ID_Fornecedor { get; set; }

        [DisplayName("Empresa")]
        public string DS_Fornecedor { get; set; }
        [DisplayName("Representate")]
        public string DS_Representante { get; set; }
        [DisplayName("Documento")]
        public string Documento { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        public Fornecedor() { }
        public Fornecedor(int iD_Fornecedor, string name, string representante, string documento, string email, string phone)
        {
            this.ID_Fornecedor = iD_Fornecedor;
            this.DS_Fornecedor = name;
            this.DS_Representante = representante;
            this.Documento = documento;
            this.Email = email;
            this.Telefone = phone;
        }

    }
}

using Brocoleendo.Models;

namespace Brocoleendo.ViewModel
{
    public class VendaViewModel
    {
        public int ProdutoSelecionadoID { get; set; }
        public List<Produto> Produtos { get; set; }
        public Venda venda { get; set; }
        public List<VendaProduto> VendaProdutos { get;set; }
    }
}

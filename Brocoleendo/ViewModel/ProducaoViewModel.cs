using Brocoleendo.Model;
using Brocoleendo.Models;

namespace Brocoleendo.ViewModel
{
    public class ProducaoViewModel
    {
        public List<AcaoQuadrante> AcaoQuadrantes { get; set; }
        public int ProdutoSelecionadoID { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}

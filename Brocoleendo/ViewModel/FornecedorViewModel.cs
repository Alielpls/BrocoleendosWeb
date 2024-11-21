using Brocoleendo.Models;

namespace Brocoleendo.ViewModel
{
    public class FornecedorViewModel
    {
        public FornecedorViewModel() { }
        public virtual Fornecedor fornecedorInfo { get; set; }
        public  List<Fornecedor> fornecedorList { get; set; }
    }
}

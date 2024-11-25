using Brocoleendo.Models;
using Brocoleendo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;



namespace Brocoleendo.Controllers
{
    public class VendaController : Controller
    {
        Uri baseAddress = new Uri("https://webapibrocoleendos20241121232909.azurewebsites.net/");
        //Uri baseAddress = new Uri("https://localhost:7196/"); 
        private readonly HttpClient _client;

        public VendaController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<Venda> Vendas = new List<Venda>();
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "getVendas").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    Vendas = JsonConvert.DeserializeObject<List<Venda>>(data);
                }
            }
            catch (Exception ex)
            {
            }



            ViewData["Title"] = "Vendas";
            return View(Vendas);
        }

        public ActionResult New()
        {
            List<Produto> produtos = new List<Produto>();

            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetProdutos").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<List<Produto>>(data);
                    produtos = produtos.Where(p => p.SALDO > 0).ToList<Produto>();
                }
            }
            catch (Exception ex)
            {
            }


            var viewModel = new VendaViewModel();
            viewModel.Produtos = produtos;

            ViewData["Title"] = "Vendas";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult newVenda([FromBody] Venda venda)
        {
            var jsonFunc = JsonConvert.SerializeObject(venda);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "insVenda", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return Json(data);
            }


            return Json(0);
        }



        [HttpPost]
        public PartialViewResult newProdutoVenda([FromBody] VendaProduto venda)
        {
            var jsonFunc = JsonConvert.SerializeObject(venda);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "insProdutoVenda", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;


                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "DetailVendaProdutos/" + venda.ID_VENDA).Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;

                List<VendaProduto> jsonFunc1 = JsonConvert.DeserializeObject<List<VendaProduto>>(data1);
                return PartialView("_table", jsonFunc1);
            }


            return null;
        }

        [HttpPost]
        public PartialViewResult dltProdutoVenda([FromBody] VendaProduto venda)
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "dltProdutoVenda/" + venda.ID_VENDA_PRODUTO).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;


                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "DetailVendaProdutos/" + venda.ID_VENDA).Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;

                List<VendaProduto> jsonFunc1 = JsonConvert.DeserializeObject<List<VendaProduto>>(data1);
                return PartialView("_table", jsonFunc1);
            }
            return null;
        }
    }
}

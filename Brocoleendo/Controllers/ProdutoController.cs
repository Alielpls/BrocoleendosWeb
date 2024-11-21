using Brocoleendo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Brocoleendo.Controllers
{
    public class ProdutoController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7196/");
        private readonly HttpClient _client;

        public ProdutoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<Produto> produtos = new List<Produto>();

            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetProdutos").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<List<Produto>>(data);
                }
            }
            catch (Exception ex)
            {
            }

            ViewData["Title"] = "Produtos";
            return View(produtos);
        }
    }
}

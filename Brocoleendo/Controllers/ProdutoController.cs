using Brocoleendo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Brocoleendo.Controllers
{
    public class ProdutoController : Controller
    {

        Uri baseAddress = new Uri("https://webapibrocoleendos20241121232909.azurewebsites.net/");
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

        
        [Route("Produto/edit/{ID}")]
        public ActionResult Edit(int ID)
        {

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "DetailProduto/" + ID).Result;
            Produto produto = new Produto();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                produto = JsonConvert.DeserializeObject<Produto>(data);
            }

            return View(produto);
        }

        [HttpPost]
        public ActionResult UpdtProduto([FromBody] Produto produto)
        {
            var jsonFunc = JsonConvert.SerializeObject(produto);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "updtProduto", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return Json(data);
            }

            return Json(false);
        }
    }
}

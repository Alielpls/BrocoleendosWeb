using Brocoleendo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Brocoleendo.Controllers
{
    public class FornecedoresController : Controller
    {
        Uri baseAddress = new Uri("https://webapibrocoleendos20241121232909.azurewebsites.net/");
        private readonly HttpClient _client;

        public FornecedoresController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetFornecedores").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    fornecedores = JsonConvert.DeserializeObject<List<Fornecedor>>(data);
                }
            }
            catch (Exception ex)
            {
            }


            ViewData["Title"] = "Fornecedores";
            return View(fornecedores);
        }


        public ActionResult Details(string id)
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"DetailFornecedor/{id}").Result;
            Fornecedor fornecedor = new Fornecedor();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                fornecedor = JsonConvert.DeserializeObject<Fornecedor>(data);
            }


            return PartialView("_tables", fornecedor);
        }


        [Route("Fornecedores/edit/{ID}")]
        public ActionResult Edit(int ID)
        {

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"DetailFornecedor/{ID}").Result;
            Fornecedor fornecedor = new Fornecedor();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                fornecedor = JsonConvert.DeserializeObject<Fornecedor>(data);
            }

            return View(fornecedor);
        }



    }
}

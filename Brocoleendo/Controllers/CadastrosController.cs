using Brocoleendo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Brocoleendo.Controllers
{
    public class CadastrosController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7196/");
        private readonly HttpClient _client;

        public CadastrosController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Produto()
        {
            Models.Produto produto = new Models.Produto();
            return View(produto);
        }
        public ActionResult Fornecedor()
        {
            return View();
        }
        public ActionResult Funcionario()
        {
            return View();
        }


        [HttpPost]
        public ActionResult InsProduto([FromBody] Produto produto)
        {
            
            var jsonFunc = JsonConvert.SerializeObject(produto);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "insProduto", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return Json(data);
            }


            return Json(false);
        }
    }
}

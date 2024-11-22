using Brocoleendo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Brocoleendo.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://webapibrocoleendos20241121232909.azurewebsites.net/");
        private readonly HttpClient _client;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar([FromBody] LoginModel Login)
        {
            Funcionario func = new Funcionario { TELEFONE = Login.Login, SENHA_CRT = Login.Senha };
            var jsonFunc = JsonConvert.SerializeObject(func);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "LoginFuncionario", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                //Funcionario loginResult = JsonConvert.DeserializeObject<Funcionario>(data);
                return Json(data);
            }


            return Json(null);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
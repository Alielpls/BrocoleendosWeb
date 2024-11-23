using Brocoleendo.Model;
using Brocoleendo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Brocoleendo.Controllers
{
    public class ProducaoController : Controller
    {

        Uri baseAddress = new Uri("https://webapibrocoleendos20241121232909.azurewebsites.net/");
        //Uri baseAddress = new Uri("https://localhost:7196/"); 
        private readonly HttpClient _client;

        public ProducaoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        
        public IActionResult Index()
        {
            List<Producao> Quadrantes = new List<Producao>();
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetQuadrantes").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    Quadrantes = JsonConvert.DeserializeObject<List<Producao>>(data);
                }
            }
            catch (Exception ex) {
            }
            
            ViewData["Title"] = "Produção";
            return View(Quadrantes);
        }

        
        public ActionResult Details(string DS)
        {
            if (DS == null)
            {
                return null;
            }
            List<AcaoQuadrante> Quadrantes = new List<AcaoQuadrante>();
                DS = DS.Trim().Substring(0, 2);
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetAcoesQuadrante/" + DS).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    Quadrantes = JsonConvert.DeserializeObject<List<AcaoQuadrante>>(data);
                }
            }
            catch (Exception ex)
            {
            }

            ViewData["Title"] = "Produção";
            return PartialView("_table", Quadrantes);
        }
    }
}

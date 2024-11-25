using Brocoleendo.Model;
using Brocoleendo.Models;
using Brocoleendo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


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
            List<Quadrante> Quadrantes = new List<Quadrante>();
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetQuadrantes").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    Quadrantes = JsonConvert.DeserializeObject<List<Quadrante>>(data);
                }
            }
            catch (Exception ex) {
            }
            
            ViewData["Title"] = "Produção";
            return View(Quadrantes);
        }

        
        public ActionResult Details(int DS)
        {
            if (DS == null)
            {
                return null;
            }
            List<AcaoQuadrante> Quadrantes = new List<AcaoQuadrante>();
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

            ProducaoViewModel viewModel = new ProducaoViewModel();
            viewModel.AcaoQuadrantes = Quadrantes;

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

            viewModel.Produtos = produtos;

            ViewData["Message"] = DS;
            //ViewData["Uso"] = ;
            ViewData["Title"] = "Produção";
            return PartialView("_table", viewModel);
        }


        [HttpPost]
        public ActionResult InsProducao([FromBody] Producao producao)
        {
            var jsonFunc = JsonConvert.SerializeObject(producao);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "InsProducao", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return Json(data);
            }


            return Json(0);
        }

        [HttpPost]
        public ActionResult dltProducao([FromBody] Producao producao)
        {
            var jsonFunc = JsonConvert.SerializeObject(producao);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "dltProducao", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return Json(data);
            }


            return Json(0);
        }


        [HttpPost]
        public ActionResult InsAcaoProducao([FromBody] AcaoQuadrante acaoQuadrante)
        {
            var jsonFunc = JsonConvert.SerializeObject(acaoQuadrante);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "InsAcaoProducao", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "GetAcoesQuadrante/" + acaoQuadrante.DS_QUADRANTE).Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;

                List<VendaProduto> jsonFunc1 = JsonConvert.DeserializeObject<List<VendaProduto>>(data1);
                return PartialView("_table", jsonFunc1);
                
            }


            return Json(0);
        }

        [HttpPost]
        public ActionResult dltAcaoProducao([FromBody] AcaoQuadrante acaoQuadrante)
        {
            var jsonFunc = JsonConvert.SerializeObject(acaoQuadrante);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "dltAcaoProducao", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "GetAcoesQuadrante/" + acaoQuadrante.DS_QUADRANTE).Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;

                List<VendaProduto> jsonFunc1 = JsonConvert.DeserializeObject<List<VendaProduto>>(data1);
                return PartialView("_table", jsonFunc1);

            }


            return Json(0);
        }


    }
}

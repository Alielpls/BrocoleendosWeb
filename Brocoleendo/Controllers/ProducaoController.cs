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
            catch (Exception ex)
            {
            }

            ViewData["Title"] = "Produção";
            return View(Quadrantes);
        }

        [HttpPost]
        public ActionResult Details([FromBody] Quadrante quadrante)
        {

            if (quadrante.ID_Localizacao_Lote == null)
            {
                return null;
            }
            List<AcaoQuadrante> AcoesQuadrante = new List<AcaoQuadrante>();
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetAcoesQuadrante/" + quadrante.ID_Localizacao_Lote).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    AcoesQuadrante = JsonConvert.DeserializeObject<List<AcaoQuadrante>>(data);
                }
            }
            catch (Exception ex)
            {
            }

            Producao producaoAtual = new Producao();

            if (quadrante.ID_Producao > 0)
            {
                try
                {
                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "DetailsProducao/" + quadrante.ID_Producao).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        producaoAtual = JsonConvert.DeserializeObject<Producao>(data);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            ProducaoViewModel viewModel = new ProducaoViewModel();
            viewModel.AcaoQuadrantes = AcoesQuadrante;
            viewModel.producao = producaoAtual;
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

            HttpResponseMessage responseProd = _client.GetAsync(_client.BaseAddress + "DetailProduto/" + producaoAtual.ID_Produto).Result;
            Produto produto = new Produto();

            if (responseProd.IsSuccessStatusCode)
            {
                string data = responseProd.Content.ReadAsStringAsync().Result;
                produto = JsonConvert.DeserializeObject<Produto>(data);
            }


            ViewData["Produto"] = produto.DS_PRODUTO;
            ViewData["ID"] = quadrante.ID_Localizacao_Lote;
            ViewData["Title"] = "Produção";
            return PartialView("_table", viewModel);
        }


        [HttpPost]
        public ActionResult InsProducao([FromBody] insProducao producao)
        {
            Producao producaoEnviar = new Producao();
            producaoEnviar.ID_LOCALIZACAO_LOTE = producao.ID_Localizacao_Lote;
            producaoEnviar.ID_Produto = producao.ID_Produto;

            var jsonFunc = JsonConvert.SerializeObject(producaoEnviar);
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
        public ActionResult dltProducao([FromBody] insProducaoFinish producao)
        {
            Producao producaoEnviar = new Producao();
            producaoEnviar.ID_PRODUCAO = producao.ID_Producao;
            producaoEnviar.KG_RENDIDO = producao.KG_Rendido;

            var jsonFunc = JsonConvert.SerializeObject(producaoEnviar);
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
        public ActionResult InsAcaoProducao([FromBody] insAcaoQuadrante acaoQuadrante)
        {
            ProducaoViewModel viewModel = new ProducaoViewModel();
            AcaoQuadrante AcaoQuadranteEnviar = new AcaoQuadrante();
            AcaoQuadranteEnviar.ID_PRODUCAO = acaoQuadrante.ID_Producao;
            AcaoQuadranteEnviar.ID_FUNC = acaoQuadrante.ID_FUNC;
            AcaoQuadranteEnviar.DS_ACAO = acaoQuadrante.DS_ACAO;


            var jsonFunc = JsonConvert.SerializeObject(AcaoQuadranteEnviar);
            var content = new StringContent(jsonFunc, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "InsAcaoProducao", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "GetAcoesQuadrante/" + acaoQuadrante.ID_QUADRANTE).Result;
                if (response1.IsSuccessStatusCode)
                {
                    string data1 = response1.Content.ReadAsStringAsync().Result;
                    List<AcaoQuadrante> jsonFunc1 = JsonConvert.DeserializeObject<List<AcaoQuadrante>>(data1);
                    viewModel.AcaoQuadrantes = jsonFunc1;


                    HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "DetailsProducao/" + acaoQuadrante.ID_Producao).Result;

                    if (response2.IsSuccessStatusCode)
                    {
                        string data2 = response2.Content.ReadAsStringAsync().Result;
                        Producao producaoAtual = JsonConvert.DeserializeObject<Producao>(data2);
                        viewModel.producao = producaoAtual;
                        viewModel.Produtos = new List<Produto>();
                        viewModel.ProdutoSelecionadoID = 0;

                        HttpResponseMessage responseProd = _client.GetAsync(_client.BaseAddress + "DetailProduto/" + producaoAtual.ID_Produto).Result;
                        Produto produto = new Produto();

                        if (responseProd.IsSuccessStatusCode)
                        {
                            string dataprod = responseProd.Content.ReadAsStringAsync().Result;
                            produto = JsonConvert.DeserializeObject<Produto>(dataprod);
                        }


                        ViewData["Produto"] = produto.DS_PRODUTO;
                        ViewData["ID"] = acaoQuadrante.ID_QUADRANTE;
                        ViewData["Title"] = "Produção";
                        return PartialView("_table", viewModel);
                    }
                }
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

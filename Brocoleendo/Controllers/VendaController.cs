using Microsoft.AspNetCore.Mvc;

namespace Brocoleendo.Controllers
{
    public class VendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

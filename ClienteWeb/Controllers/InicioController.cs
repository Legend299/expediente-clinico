using Microsoft.AspNetCore.Mvc;

namespace ClienteWeb.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

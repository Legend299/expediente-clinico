using Microsoft.AspNetCore.Mvc;

namespace ClienteWeb.Controllers
{
    public class ArchivoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

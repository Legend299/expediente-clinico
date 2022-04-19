using Microsoft.AspNetCore.Mvc;

namespace ClienteWeb.Controllers
{
    public class MedicoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

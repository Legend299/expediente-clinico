using expediente_clinico.Models;
using expedienteWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace expedienteWeb.Controllers
{
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;

        public InicioController(ILogger<InicioController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*var httpClient = new HttpClient();
            var json = "";
            //var json = await httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Medico");
            if (httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Medico").IsCompleted)
            {
                json = await httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Medico");
            }
            else {
                json = await httpClient.GetStringAsync("http://192.168.1.69:8891/api/Medico");
            }
            var medicoList = JsonConvert.DeserializeObject<List<Medico>>(json);*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

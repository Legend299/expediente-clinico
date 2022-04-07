using expediente_clinico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace expedienteWeb.Controllers
{
    public class ExpedienteController : Controller
    {
        public IActionResult Index(Expediente expediente)
        {
            return View(expediente);
        }

        public IActionResult EditarExpediente(Expediente expediente) 
        {
            return View(expediente);
        }

        public IActionResult ExpedientePdf() 
        {
            return View();
        }

        public async Task<IActionResult> Editar() 
        {
            //Expediente expediente = new Expediente();
            Console.WriteLine("EDITAR EXPEDIENTE");
            var httpClient = new HttpClient();
            var json = "";

            if (httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Expediente/" + HttpContext.Session.GetString("Curp")).IsCompleted)
            {
                json = await httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Expediente/" + HttpContext.Session.GetString("Curp"));
            }
            else
            {
                json = await httpClient.GetStringAsync("http://192.168.1.69:8891/api/Expediente/" + HttpContext.Session.GetString("Curp"));
            }
            Expediente expediente = JsonConvert.DeserializeObject<Expediente>(json);

             return RedirectToAction("EditarExpediente", expediente);
        }
    }
}

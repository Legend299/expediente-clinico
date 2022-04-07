using expediente_clinico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<IActionResult> ActualizarExpediente() 
        {
            //--------------------
            Expediente expediente = new Expediente();
            expediente.Imagen = "https://beporsam.ir/wp-content/uploads/2017/03/user.png";
            expediente.Nombre = Request.Form["nombre"];
            expediente.Apellido = Request.Form["apellido"];
            expediente.Telefono = Request.Form["telefono"];
            expediente.FechaDeNacimiento = Convert.ToDateTime(Request.Form["fechadenacimiento"]);
            expediente.Direccion = Request.Form["direccion"];
            expediente.Sexo = Convert.ToBoolean(Request.Form["sexo"]);
            expediente.Curp = Request.Form["curp"];

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(expediente);

            //httpClient.BaseAddress = new Uri("http://legend.zapto.org:8891/api/Usuario");

            if (httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Usuario").IsCompleted)
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://legend.zapto.org:8891/");
            }
            else
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://192.168.1.69:8891/");
            }
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync("api/Expediente/"+expediente.Curp, httpContent);
            Console.WriteLine("CODIGO: " + response);
            await Task.Delay(3000);
            //--------------------
            return RedirectToAction("SolicitarExpediente", "Perfil");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using webservice1.Models;

namespace ClienteWeb.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private int _idExpediente = 0;

        public ConsultaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /*
         * Páginas
         */
        public async Task<IActionResult> Ver()
        {

            //Validar ver consulta
            if (HttpContext.Session.GetInt32("Expediente") == null || HttpContext.Session.GetInt32("Expediente") == 0) {
                return RedirectToAction("Index", "Inicio");
            }
            _idExpediente = (int)HttpContext.Session.GetInt32("Expediente");
            List<Consulta> listConsulta = await SolicitarListaConsulta();
            return View(listConsulta);
        }

        /*
         * Acciones
         */

        // Validar expediente
        public async Task<List<Consulta>> SolicitarListaConsulta()
        {
            //Validar ver consulta
            if (_idExpediente == 0)
                Ver();

            int idExpediente = _idExpediente;

            var json = "";

            var httpClient = _httpClientFactory.CreateClient("api");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
            json = await httpClient.GetStringAsync("api/Consulta/" + Convert.ToString(idExpediente));

            List<Consulta> listaConsulta = JsonConvert.DeserializeObject<List<Consulta>>(json);

            return listaConsulta;
        }

        // Eliminar consulta
        // Falta Validar
        public async Task<IActionResult> EliminarConsulta(int id) 
        {

            var httpClient = _httpClientFactory.CreateClient("api");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
            HttpResponseMessage response = await httpClient.DeleteAsync("api/Consulta/" + Convert.ToString(id));

            if (response.IsSuccessStatusCode)
                TempData["Mensaje"] = "Consulta eliminada con éxito.";
            else
                TempData["ErrorMensaje"] = "No se ha podido eliminar la consulta.";

            return RedirectToAction("Ver");
        }

        public async Task<IActionResult> AgregarConsulta() 
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetString("Rol") != "2")
                return RedirectToAction("Index", "Inicio");

            Consulta consulta = new Consulta
            {
                Fecha = DateOnly.FromDateTime(Convert.ToDateTime(Request.Form["fecha"])),
                Medico = HttpContext.Session.GetString("Nombre"),
                IdTipoConsulta = Convert.ToInt32(Request.Form["tipoconsulta"]),
                //IdTipoConsulta = 1,
                IdExpediente = (int)HttpContext.Session.GetInt32("Expediente"),
                Diagnostico = Request.Form["diagnostico"]
               
            };

            var json = JsonConvert.SerializeObject(consulta);

            var httpClient = _httpClientFactory.CreateClient("api");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Consulta/", httpContent);

            if (response.IsSuccessStatusCode)
                TempData["Mensaje"] = "Consulta agregada con éxito.";
            else
                TempData["ErrorMensaje"] = "No se ha podido agregar la consulta.";

            return RedirectToAction("Ver");

        }

    }
}

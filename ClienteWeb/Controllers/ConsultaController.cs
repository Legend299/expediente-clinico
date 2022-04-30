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
        private readonly IOptions<ConexionApi> _conexionApi;
        private int _idExpediente = 0;

        public ConsultaController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
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
                using (var httpClient = new HttpClient())
                {
                    if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Consulta/" + Convert.ToString(idExpediente)).IsCompleted)
                        json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Consulta/" + Convert.ToString(idExpediente));
                    else
                        json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Consulta/" + Convert.ToString(idExpediente));

                }

                List<Consulta> listaConsulta = JsonConvert.DeserializeObject<List<Consulta>>(json);

                return listaConsulta;
        }


        // Eliminar consulta
        // Falta Validar
        public async Task<IActionResult> EliminarConsulta(int id) 
        {
            var json = "";

            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario").IsCompleted)
                    await httpClient.DeleteAsync(_conexionApi.Value.conexionPublica + "/Consulta/" + Convert.ToString(id));
                else
                    await httpClient.DeleteAsync(_conexionApi.Value.conexionPrivada + "/Consulta/" + Convert.ToString(id));
            }

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

            var httpClient = new HttpClient();

            if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario").IsCompleted)
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_conexionApi.Value.conexionPublica);
            }
            else
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_conexionApi.Value.conexionPrivada);
            }
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

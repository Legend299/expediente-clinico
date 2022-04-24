using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
    }
}

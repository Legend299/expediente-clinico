using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using webservice1.Models;

namespace ClienteWeb.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;

        public ConsultaController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        /*
         * Páginas
         */
        public async Task<IActionResult> Ver()
        {
            List<Consulta> listConsulta = await SolicitarListaConsulta((int)HttpContext.Session.GetInt32("Expediente"));
            return View(listConsulta);
        }

        /*
         * Acciones
         */

        // Validar expediente
        public async Task<List<Consulta>> SolicitarListaConsulta(int idExpediente)
        {

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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using webservice1.Models;
using webservice1.Models.DTO;

namespace ClienteWeb.Controllers
{
    public class ExpedienteController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;

        public ExpedienteController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        /*
         * Páginas
         */
        public async Task<IActionResult> VisualizarExpediente()
        {
            ExpedienteDTO expediente = await SolicitarExpediente((int)HttpContext.Session.GetInt32("Expediente"));
            return View(expediente);
        }

        public async Task<IActionResult> Editar() 
        {
            ExpedienteDTO expediente = await SolicitarExpediente((int)HttpContext.Session.GetInt32("Expediente"));
            return View(expediente);
        }

        /*
         * Acciones
         */

        // Validar expediente
        public async Task<ExpedienteDTO> SolicitarExpediente(int idExpediente) 
        {

            var json = "";
            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/"+Convert.ToString(idExpediente)).IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString(idExpediente));
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Expediente/" + Convert.ToString(idExpediente));

            }

            ExpedienteDTO expedienteUsuario = JsonConvert.DeserializeObject<ExpedienteDTO>(json);

            return expedienteUsuario;
        }

    }
}

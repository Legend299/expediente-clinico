using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using webservice1.Models;

namespace ClienteWeb.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;

        public MedicoController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        /*
         *  Páginas
         */
        public async Task<IActionResult> Usuarios()
        {
            List<Usuario> listaUsuarios = await ListarUsuarios();
            return View(listaUsuarios);
        }

        /*
         * Acciones
         */
        public async Task<List<Usuario>> ListarUsuarios() 
        {
            var json = "";
            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/").IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/");
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Usuario/");

            }

            List<Usuario> listaUsuario = JsonConvert.DeserializeObject<List<Usuario>>(json);

            return listaUsuario;
        }
    }
}

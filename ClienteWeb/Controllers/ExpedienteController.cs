using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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

        public IActionResult Tramitar() 
        {
            return View();
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

        public async Task<IActionResult> ModificarExpediente() 
        {

            Expediente expediente = new Expediente();
            
            
            expediente.Imagen = "https://beporsam.ir/wp-content/uploads/2017/03/user.png";
            expediente.IdExpediente = (int)HttpContext.Session.GetInt32("Expediente");
            expediente.Nombre = Request.Form["nombre"];
            expediente.Apellido = Request.Form["apellido"];
            expediente.Telefono = Request.Form["telefono"];
            
            DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(Request.Form["fechadenacimiento"]));
            expediente.FechaDeNacimiento = dateOnly;

            //expediente.Estado.IdEstado = Convert.ToInt32(Request.Form["estado"]);
            //expediente.Municipio.IdMunicipio = Convert.ToInt32(Request.Form["municipio"]);
            /*expediente.Estado = new EstadoDTO{
                IdEstado = Convert.ToInt32(Request.Form["estado"])
            };*/

            /*expediente.Municipio = new MunicipioDTO{
                IdMunicipio = Convert.ToInt32(Request.Form["municipio"])
            };*/
            expediente.IdEstado = Convert.ToInt32(Request.Form["estado"]);
            expediente.IdMunicipio = Convert.ToInt32(Request.Form["municipio"]);

            expediente.Sexo = Convert.ToBoolean(Request.Form["sexo"]);
            expediente.Curp = Request.Form["curp"];

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(expediente);

            if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString((int)HttpContext.Session.GetInt32("Expediente"))).IsCompleted)
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
            HttpResponseMessage response = await httpClient.PutAsync("api/Expediente/", httpContent);
            Console.WriteLine("CODIGO: " + response);
            //httpClient.Dispose();

            //await Task.Delay(3000);

            return RedirectToAction("VisualizarExpediente");
        }

        public async Task<IActionResult> AgregarExpediente()
        {
            Expediente expediente = new Expediente();


            expediente.Imagen = "https://beporsam.ir/wp-content/uploads/2017/03/user.png";
            expediente.Nombre = Request.Form["nombre"];
            expediente.Apellido = Request.Form["apellido"];
            expediente.Telefono = Request.Form["telefono"];

            DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(Request.Form["fechadenacimiento"]));
            expediente.FechaDeNacimiento = dateOnly;

            expediente.IdEstado = Convert.ToInt32(Request.Form["estado"]);
            expediente.IdMunicipio = Convert.ToInt32(Request.Form["municipio"]);

            expediente.Sexo = Convert.ToBoolean(Request.Form["sexo"]);
            expediente.Curp = Request.Form["curp"];

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(expediente);

            if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString(1)).IsCompleted)
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
            HttpResponseMessage response = await httpClient.PostAsync("api/Expediente/", httpContent);
            Console.WriteLine("Mensaje: " + response.Content);

            Expediente expedienteUsuario = JsonConvert.DeserializeObject<Expediente>(await response.Content.ReadAsStringAsync());

            //await Task.Delay(1000);
            AgregarExpedienteUsuario(expedienteUsuario.IdExpediente);
            HttpContext.Session.SetInt32("Expediente", expedienteUsuario.IdExpediente);

            //httpClient.Dispose();

            //await Task.Delay(3000);

            return RedirectToAction("VisualizarExpediente");
        }

        public async void AgregarExpedienteUsuario(int IdExpediente)
        {
            Usuario usuario = new Usuario();

            usuario.IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            usuario.Correo = HttpContext.Session.GetString("Correo");
            usuario.Password = HttpContext.Session.GetString("Password");
            usuario.IdRol = Convert.ToInt32(HttpContext.Session.GetString("Rol"));
            usuario.IdExpediente = IdExpediente;
            usuario.Activo = true;


            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(usuario);

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
            HttpResponseMessage response = await httpClient.PutAsync("api/Usuario/", httpContent);
            Console.WriteLine("CODIGO USUARIO: " + response);

        }

    }
}

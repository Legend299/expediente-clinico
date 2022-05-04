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
        private readonly IHttpClientFactory _httpClientFactory;

        private int _idExpediente = 0;
        private int _idUsuario = 0;

        public ExpedienteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /*
         * Páginas
         */
        public async Task<IActionResult> Ver()
        {
            if (HttpContext.Session.GetString("Id") == null)
                return RedirectToAction("Index", "Inicio");

            _idExpediente = (int)HttpContext.Session.GetInt32("Expediente");
            ExpedienteDTO expediente = await SolicitarExpediente();

            return View(expediente);
        }

        public async Task<IActionResult> Editar() 
        {
            if (HttpContext.Session.GetString("Id") == null)
                return RedirectToAction("Index", "Inicio");

            _idExpediente = (int)HttpContext.Session.GetInt32("Expediente");
            ExpedienteDTO expediente = await SolicitarExpediente();
            return View(expediente);
        }

        public IActionResult Tramitar() 
        {
            if (HttpContext.Session.GetString("Id") == null || (int)HttpContext.Session.GetInt32("Expediente") != 0)
                return RedirectToAction("Index", "Inicio");

            return View();
        }



        /*
         * Acciones
         */

        // Validar expediente
        public async Task<ExpedienteDTO> SolicitarExpediente() 
        {

            if (_idExpediente == 0)
                Tramitar();

            var json = "";

            var httpClient = _httpClientFactory.CreateClient("api");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
            json = await httpClient.GetStringAsync("api/Expediente/"+Convert.ToString(_idExpediente));

            ExpedienteDTO expedienteUsuario = JsonConvert.DeserializeObject<ExpedienteDTO>(json);
            HttpContext.Session.SetString("Nombre", expedienteUsuario.Nombre);

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

            expediente.IdEstado = Convert.ToInt32(Request.Form["estado"]);
            expediente.IdMunicipio = Convert.ToInt32(Request.Form["municipio"]);

            expediente.Sexo = Convert.ToBoolean(Request.Form["sexo"]);
            expediente.Curp = Request.Form["curp"];

            //var httpClient = new HttpClient();
            var httpClient = _httpClientFactory.CreateClient("api"); 
            var json = JsonConvert.SerializeObject(expediente);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync("api/Expediente/", httpContent);

            HttpContext.Session.SetString("Nombre", expediente.Nombre);

            return RedirectToAction("Ver");
        }

        public async Task<IActionResult> AgregarExpediente()
        {

            if (Request.Form["nombre"] == "")
                Ver();

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

            //var httpClient = new HttpClient();
            var httpClient = _httpClientFactory.CreateClient("api");

            var json = JsonConvert.SerializeObject(expediente);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Expediente/", httpContent);

            Expediente expedienteUsuario = JsonConvert.DeserializeObject<Expediente>(await response.Content.ReadAsStringAsync());

            _idExpediente = expedienteUsuario.IdExpediente;
            AgregarExpedienteUsuario();
            HttpContext.Session.SetInt32("Expediente", expedienteUsuario.IdExpediente);
            HttpContext.Session.SetString("Nombre", expedienteUsuario.Nombre);

            return RedirectToAction("Ver");
        }

        public async void AgregarExpedienteUsuario()
        {
            if (_idExpediente == 0)
                Tramitar();

            Usuario usuario = new Usuario();

            usuario.IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            usuario.Correo = HttpContext.Session.GetString("Correo");
            usuario.Password = HttpContext.Session.GetString("Password");
            usuario.IdRol = Convert.ToInt32(HttpContext.Session.GetString("Rol"));
            usuario.IdExpediente = _idExpediente;
            usuario.Activo = true;

            var httpClient = _httpClientFactory.CreateClient("api");
            var json = JsonConvert.SerializeObject(usuario);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync("api/Usuario/", httpContent);

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using webservice1.Models;
using webservice1.Models.DTO;

namespace ClienteWeb.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;
        private int _idExpediente = 0;
        public PerfilController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        /*
         *  Páginas principales
         */
        public IActionResult InicioSesion()
        {
            // Denegar acceso a login si ya ha iniciado sesión;
            if (HttpContext.Session.GetString("Id") != null)
                return RedirectToAction("Index", "Inicio");

            return View();
        }

        // Validar registrar cuenta
        public IActionResult Registrar()
        {
            // Denegar Registrar cuenta si ya ha iniciado sesión
            if (HttpContext.Session.GetString("Id") != null)
                return RedirectToAction("Index", "Inicio");

            return View(); 
        }

        /*
         *  Acciones 
         */

        public async Task<IActionResult> Acceder() 
        {
            // Denegar Registrar cuenta si ya ha iniciado sesión
            if (HttpContext.Session.GetString("Id") != null)
                return RedirectToAction("Index", "Inicio");

            var json = "";
            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario").IsCompleted)
                {
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario");
                    HttpContext.Session.SetString("Conexion", "publica");
                } else {
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Usuario");

                    HttpContext.Session.SetString("Conexion", "privada");
                }
            }

            List<Usuario> usuarioList = JsonConvert.DeserializeObject<List<Usuario>>(json);

            string correoForm = Request.Form["correo"];
            string contraForm = Request.Form["contrasena1"];

            foreach (Usuario user in usuarioList)
            {
                if (user.Correo.Equals(correoForm) && user.Password.Equals(contraForm))
                {
                    HttpContext.Session.SetString("Id", Convert.ToString(user.IdUsuario));
                    HttpContext.Session.SetString("Correo", user.Correo);
                    HttpContext.Session.SetInt32("Expediente", (int)user.IdExpediente);
                    HttpContext.Session.SetString("Password", user.Password);
                    HttpContext.Session.SetString("Rol", Convert.ToString(user.IdRol));
                    Console.WriteLine("ID EXPE: "+user.IdExpediente);

                    if (user.IdExpediente > 0) 
                    {
                        _idExpediente = (int)user.IdExpediente;
                        ExpedienteDTO expediente = await SolicitarExpediente();
                        HttpContext.Session.SetString("Nombre", expediente.Nombre);
                    }

                    return RedirectToAction("Index", "Inicio");
                }
            }

            TempData["Message"] = "Verifica que el correo y la contraseña estén escritos correctamente.";
            TempData["Correo"] = correoForm;

            return RedirectToAction("InicioSesion");
        }

        public async Task<IActionResult> RegistrarCuenta() 
        {
            // Denegar Registrar cuenta si ya ha iniciado sesión
            if (HttpContext.Session.GetString("Id") != null)
                return RedirectToAction("Index", "Inicio");

            if (Request.Form["contrasena1"] != Request.Form["contrasena2"])
            {
                TempData["Mensaje"] = "Las contraseñas no coinciden";

                string correo = Request.Form["correo"];
                TempData["Correo"] = correo;
                return RedirectToAction("Registrar");
            }

            Usuario usuario = new Usuario();
            usuario.Correo = Request.Form["correo"];
            usuario.Password = Request.Form["contrasena1"];
            usuario.IdRol = 3;
            usuario.Activo = true;

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(usuario);

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
            HttpResponseMessage response = await httpClient.PostAsync("api/Usuario", httpContent);
            Console.WriteLine("CODIGO: " + response);

            return RedirectToAction("InicioSesion");
        }

        // Validar cerrar sesión
        public IActionResult CerrarSesion() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("InicioSesion");
        }

        public async Task<ExpedienteDTO> SolicitarExpediente()
        {
            if (_idExpediente == 0)
                InicioSesion();
                //throw new Exception("Error");

            var json = "";
            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString(_idExpediente)).IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString(_idExpediente));
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Expediente/" + Convert.ToString(_idExpediente));

            }

            ExpedienteDTO expedienteUsuario = JsonConvert.DeserializeObject<ExpedienteDTO>(json);

            return expedienteUsuario;
        }
    }
}

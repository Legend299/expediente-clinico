using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using webservice1.Models;
using webservice1.Models.DTO;
using webservice1.Tools;

namespace ClienteWeb.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;
        //private readonly IHttpClientFactory _httpClientFactory;

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

            //var json = "";
            //using (var httpClient = new HttpClient())
            //{
            //    if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario").IsCompleted)
            //    {
            //        HttpContext.Session.SetString("Conexion", "publica");
            //        json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario");

            //    } else {
            //        HttpContext.Session.SetString("Conexion", "privada");
            //        json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Usuario");

            //    }
            //}

            //List<Usuario> usuarioList = JsonConvert.DeserializeObject<List<Usuario>>(json);

            //string correoForm = Request.Form["correo"];
            //string contraForm = Request.Form["contrasena1"];

            //// Encriptado de contraseña

            //string sha256 = Encrypt.GetSHA256(contraForm);;

            //foreach (Usuario user in usuarioList)
            //{
            //    if (user.Correo.Equals(correoForm) && user.Password.Equals(sha256))
            //    {
            //        HttpContext.Session.SetString("Id", Convert.ToString(user.IdUsuario));
            //        HttpContext.Session.SetString("Correo", user.Correo);
            //        HttpContext.Session.SetInt32("Expediente", (int)user.IdExpediente);
            //        HttpContext.Session.SetString("Password", sha256);
            //        HttpContext.Session.SetString("Rol", Convert.ToString(user.IdRol));
            //        Console.WriteLine("ID EXPE: "+user.IdExpediente);

            //        if (user.IdExpediente > 0) 
            //        {
            //            _idExpediente = (int)user.IdExpediente;
            //            ExpedienteDTO expediente = await SolicitarExpediente();
            //            HttpContext.Session.SetString("Nombre", expediente.Nombre);
            //        }

            //        return RedirectToAction("Index", "Inicio");
            //    }
            //}


            string correoForm = Request.Form["correo"];
            string contraForm = Request.Form["contrasena1"];


            // Encriptado de contraseña

            string sha256 = Encrypt.GetSHA256(contraForm);;

            Usuario usuario = new Usuario
            {
                Correo = correoForm,
                Password = sha256
            };

            var httpClient = new HttpClient();
            //var httpClient = _httpClientFactory.CreateClient("publico");
            httpClient.BaseAddress = new Uri(_conexionApi.Value.conexionPublica);

            var json = JsonConvert.SerializeObject(usuario);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
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
            HttpResponseMessage response = await httpClient.PostAsync("api/Usuario/Login", httpContent);

            if (response.IsSuccessStatusCode) 
            {
                var respuesta = response.Content.ReadAsStringAsync().Result;
                UsuarioToken user = JsonConvert.DeserializeObject<UsuarioToken>(respuesta);

                HttpContext.Session.SetString("Id", Convert.ToString(user.IdUsuario));
                HttpContext.Session.SetString("Correo", user.Correo);
                HttpContext.Session.SetInt32("Expediente", (int)user.IdExpediente);
                HttpContext.Session.SetString("Password", sha256);
                HttpContext.Session.SetString("Rol", Convert.ToString(user.IdRol));
                HttpContext.Session.SetString("Token", user.Token);
                Console.WriteLine("ID EXPE: " + user.IdExpediente);

                if (user.IdExpediente > 0)
                {
                    _idExpediente = (int)user.IdExpediente;
                    ExpedienteDTO expediente = await SolicitarExpediente();
                    HttpContext.Session.SetString("Nombre", expediente.Nombre);
                }

                return RedirectToAction("Index", "Inicio");
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

            // Encriptado de contraseña

            string sha256 = Encrypt.GetSHA256(usuario.Password);
            usuario.Password = sha256;

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(usuario);

            httpClient.BaseAddress = new Uri("http://legend.zapto.org:8891/api/Usuario");

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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

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

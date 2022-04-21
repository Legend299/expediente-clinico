using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using webservice1.Models;

namespace ClienteWeb.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;

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
        public IActionResult RegistrarCuenta()
        {
            return View(); 
        }

        /*
         *  Acciones 
         */

        public async Task<IActionResult> Acceder() 
        {
            var json = "";
            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario").IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario");
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Usuario");

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

                    return RedirectToAction("Index", "Inicio");
                }
            }

            TempData["Message"] = "Verifica que el correo y la contraseña estén escritos correctamente.";
            TempData["Correo"] = correoForm;

            return RedirectToAction("InicioSesion");
        }

        public IActionResult Registrar() 
        {
            return View();
        }

        // Validar cerrar sesión
        public IActionResult CerrarSesion() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("InicioSesion");
        }
    }
}

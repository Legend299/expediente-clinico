using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using webservice1.Models;
using webservice1.Models.DTO;

namespace ClienteWeb.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;
        private int _IdMedico = 0;

        public MedicoController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        /*
         *  Páginas
         */
        public async Task<IActionResult> Usuarios()
        {

            if (HttpContext.Session.GetString("Rol") == null && HttpContext.Session.GetString("Rol") != "2" && HttpContext.Session.GetString("Rol") != "1")
                return RedirectToAction("Index", "Inicio");

            List<Usuario> listaUsuarios = await ListarUsuarios();
            return View(listaUsuarios);
        }

        public async Task<IActionResult> MisPacientes() 
        {
            if (HttpContext.Session.GetString("Rol") == null && HttpContext.Session.GetString("Rol") != "2" && HttpContext.Session.GetString("Rol") != "1")
                return RedirectToAction("Index", "Inicio");

            _IdMedico = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            List<ExpedientesPermiso> listaPacientes = await ListarPacientes();
            return View(listaPacientes);
        }

        // Expediente Usuario

        public async Task<IActionResult> Expediente(int IdExpediente) 
        {

            //Validar
            if (HttpContext.Session.GetInt32("Expediente") == null || HttpContext.Session.GetString("Rol") != "2")
                Usuarios();

            //Validar MIS PACIENTES

            
            _IdMedico = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            List<ExpedientesPermiso> listaPacientes = await ListarPacientes();
            bool valido = false;

            foreach (ExpedientesPermiso e in listaPacientes)
            {
                if (e.IdExpediente == IdExpediente) 
                {
                    valido = true;
                }
                    
            }

            if (valido == false)
                return RedirectToAction("MisPacientes");
            //

            TempData["ExpedienteUsuario"] = IdExpediente;

            var json = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString(IdExpediente)).IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString(IdExpediente));
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Expediente/" + Convert.ToString(IdExpediente));

            }

            ExpedienteDTO expedienteUsuario = JsonConvert.DeserializeObject<ExpedienteDTO>(json);

            return View(expedienteUsuario);
        }

        // Consultas Usuario

        public async Task<IActionResult> Consultas(int IdExpediente) 
        {
            if (TempData["ExpedienteUsuario"] != null)
                IdExpediente = Convert.ToInt32(TempData["ExpedienteUsuario"]);

            //Validar
            if (HttpContext.Session.GetInt32("Expediente") == null || HttpContext.Session.GetString("Rol") != "2")
                Usuarios();

            //Validar MIS PACIENTES


            _IdMedico = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            List<ExpedientesPermiso> listaPacientes = await ListarPacientes();
            bool valido = false;

            foreach (ExpedientesPermiso e in listaPacientes)
            {
                if (e.IdExpediente == IdExpediente)
                {
                    valido = true;
                }

            }

            if (valido == false)
                return RedirectToAction("MisPacientes");
            //

            TempData["ExpedienteUsuario"] = IdExpediente;

            var json = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Consulta/" + Convert.ToString(IdExpediente)).IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Consulta/" + Convert.ToString(IdExpediente));
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Consulta/" + Convert.ToString(IdExpediente));

            }

            List<Consulta> listaConsulta = JsonConvert.DeserializeObject<List<Consulta>>(json);

            return View(listaConsulta);
        }

        // Documentos Usuario
        public async Task<IActionResult> Documentos(int IdExpediente)
        {
            if(TempData["ExpedienteUsuario"] != null)
            IdExpediente = Convert.ToInt32(TempData["ExpedienteUsuario"]);

            //Validar
            if (HttpContext.Session.GetInt32("Expediente") == null || HttpContext.Session.GetString("Rol") != "2")
                Usuarios();

            //Validar MIS PACIENTES


            _IdMedico = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            List<ExpedientesPermiso> listaPacientes = await ListarPacientes();
            bool valido = false;

            foreach (ExpedientesPermiso e in listaPacientes)
            {
                if (e.IdExpediente == IdExpediente)
                {
                    valido = true;
                }

            }

            if (valido == false)
                return RedirectToAction("MisPacientes");
            //

            TempData["ExpedienteUsuario"] = IdExpediente;

            var json = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Documento/" + Convert.ToString(IdExpediente)).IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Documento/" + Convert.ToString(IdExpediente));
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Documento/" + Convert.ToString(IdExpediente));

            }

            List<Documento> listaDocumento = JsonConvert.DeserializeObject<List<Documento>>(json);

            return View(listaDocumento);
        }


        public IActionResult Perfil() 
        {
            if (HttpContext.Session.GetString("Rol") == null)
                return RedirectToAction("Usuarios");

            if (HttpContext.Session.GetString("Rol").Equals("3")) 
                return RedirectToAction("Agregar");

            return View();
        }

        public async Task<IActionResult> Agregar() 
        {
            if (HttpContext.Session.GetString("Rol") == null || HttpContext.Session.GetString("Rol").Equals("2") || HttpContext.Session.GetString("Rol").Equals("1"))
                return RedirectToAction("Index", "Inicio");

            List<Especialidade> listaEspecialidades = await ListarEspecialidades();
            return View(listaEspecialidades); 
        }

        /*
         * Acciones
         */
        public async Task<List<Usuario>> ListarUsuarios() 
        {

            if (HttpContext.Session.GetString("Rol") == null && HttpContext.Session.GetString("Rol") != "2" && HttpContext.Session.GetString("Rol") != "1")
                Usuarios();

            var json = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/").IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/");
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Usuario/");

            }

            List<Usuario> listaUsuario = JsonConvert.DeserializeObject<List<Usuario>>(json);

            return listaUsuario;
        }

        public async Task<List<ExpedientesPermiso>> ListarPacientes() 
        {
            if (HttpContext.Session.GetString("Rol") == null && HttpContext.Session.GetString("Rol") != "2" && HttpContext.Session.GetString("Rol") != "1" || _IdMedico == 0)
                Usuarios();

            var json = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/").IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/Permiso/medico/pacientes/"+ _IdMedico);
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Usuario/Permiso/medico/pacientes/"+ _IdMedico);

            }

            List<ExpedientesPermiso> listaUsuario = JsonConvert.DeserializeObject<List<ExpedientesPermiso>>(json);

            return listaUsuario;
        }

        // Validar
        public async Task<List<Especialidade>> ListarEspecialidades() 
        {
            var json = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/").IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Medico/Especialidades");
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Medico/Especialidades");
            }

            List<Especialidade> listaEspecialidades = JsonConvert.DeserializeObject<List<Especialidade>>(json);

            return listaEspecialidades;

        }

        // Validar
        public async Task<IActionResult> SolicitarPermiso(int IdExpediente) 
        {
            //Validar
            if (HttpContext.Session.GetInt32("Expediente") == null || HttpContext.Session.GetString("Rol") != "2")
                Usuarios();

            ExpedientesPermiso expedientesPermiso = new ExpedientesPermiso
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                IdExpediente = IdExpediente,
                PermisoMedico = false,
                PermisoUsuario = false
            };


            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(expedientesPermiso);

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
            HttpResponseMessage response = await httpClient.PostAsync("api/usuario/permiso", httpContent);
            //HttpResponseMessage response = await httpClient.PostAsync(httpContent);
            Console.WriteLine("CODIGO: " + response);

            TempData["Solicitud"] = "Por favor verifica tu identidad en la aplicación móvil, para llevar a cabo la solicitud del expediente.";

            return RedirectToAction("Usuarios");


        }

        public async Task<IActionResult> SubirDocumento(IFormFile archivo)
        {
            //if (archivo == null)
            //    MisPacientes();

            string idExpediente = Request.Form["IdExpedienteUsuario"];

            try
            {
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    multipartFormContent.Add(new StringContent(archivo.FileName), name: "Nombre");
                    multipartFormContent.Add(new StringContent(System.IO.Path.GetExtension(archivo.FileName)), name: "Extension");
                    multipartFormContent.Add(new StringContent(HttpContext.Session.GetString("Nombre")), name: "Medico");
                    multipartFormContent.Add(new StringContent(Convert.ToString(archivo.Length)), name: "Peso");
                    multipartFormContent.Add(new StringContent(idExpediente), name: "IdExpediente");

                    var filestreamContent = new StreamContent(archivo.OpenReadStream());
                    filestreamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);

                    multipartFormContent.Add(filestreamContent, name: "Archivo", fileName: archivo.FileName);

                    var response = await cliente.PostAsync(_conexionApi.Value.conexionPrivada + "/Documento", multipartFormContent);
                    //var test = await response.Content.ReadAsStringAsync();
                    // Código
                    //Console.WriteLine(test);
                }
                TempData["Mensaje"] = "Se ha subido: " + archivo.FileName;
                TempData["ExpedienteUsuario"] = idExpediente;
                return RedirectToAction("Documentos");
            }
            catch (Exception e)
            {
                TempData["ErrorMensaje"] = "No se ha podido subir el archivo.";
                TempData["ExpedienteUsuario"] = idExpediente;
                return RedirectToAction("Documentos");
            }

        }

        public async Task<IActionResult> Verificar() 
        {

            string cedula = Request.Form["cedula"];
            string especialidad = Request.Form["especialidad"];

            var json = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario/").IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Medico/");
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Medico/");
            }

            List<Medico> listaMedico = JsonConvert.DeserializeObject<List<Medico>>(json);
            
            bool verificar = false;

            foreach (Medico e in listaMedico) 
            {
                if (e.Cedula.Equals(cedula))
                {
                    verificar = true;
                    e.IdEspecialidad = Convert.ToInt32(especialidad);
                    e.IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    HttpResponseMessage response = await ModificarMedico(e);
                    if (response.IsSuccessStatusCode)
                    {
                        ModificarUsuario();
                    }
                    break;
                }
            }

            if (verificar == false) 
            {
                //    
                //    //    HttpContext.Session.SetString("Rol", Convert.ToString(3));
                TempData["Message"] = "Verifica que tu cédula profesional está escrita correctamente";
                return RedirectToAction("Agregar");
            }

            HttpContext.Session.SetString("Rol", Convert.ToString(2));
            return RedirectToAction("Index", "Inicio");
        }

        public async Task<HttpResponseMessage> ModificarMedico(Medico medico) 
        {
            
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(medico);

            if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/" + Convert.ToString((int)HttpContext.Session.GetInt32("Expediente"))).IsCompleted)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
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
            HttpResponseMessage response = await httpClient.PutAsync("api/Medico/", httpContent);

            return response;

            //if (response.IsSuccessStatusCode) 
            //    ModificarUsuario((int)medico.IdUsuario);

        }

        public async Task ModificarUsuario() 
        {

            Usuario usuario = new Usuario
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                Correo = HttpContext.Session.GetString("Correo"),
                Password = HttpContext.Session.GetString("Password"),
                IdRol = 2,
                IdExpediente = HttpContext.Session.GetInt32("Expediente"),
                Activo = true
            };

            //

            var httpClient = new HttpClient();

            usuario.IdRol = 2;

            var _json = JsonConvert.SerializeObject(usuario);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
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
            HttpContent httpContent = new StringContent(_json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync("api/Usuario/", httpContent);
        }

        // Eliminar consulta
        // Falta Validar
        public async Task<IActionResult> EliminarConsulta(int id, int idexpediente)
        {
            var json = "";

            TempData["ExpedienteUsuario"] = idexpediente;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Usuario").IsCompleted)
                    await httpClient.DeleteAsync(_conexionApi.Value.conexionPublica + "/Consulta/" + Convert.ToString(id));
                else
                    await httpClient.DeleteAsync(_conexionApi.Value.conexionPrivada + "/Consulta/" + Convert.ToString(id));
            }

            return RedirectToAction("Consultas");
        }

        public async Task<IActionResult> AgregarConsulta()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetString("Rol") != "2")
                return RedirectToAction("Index", "Inicio");

            int idExpediente = Convert.ToInt32(Request.Form["idexpediente"]);

            Consulta consulta = new Consulta
            {
                Fecha = DateOnly.FromDateTime(Convert.ToDateTime(Request.Form["fecha"])),
                Medico = HttpContext.Session.GetString("Nombre"),
                IdTipoConsulta = Convert.ToInt32(Request.Form["tipoconsulta"]),
                //IdTipoConsulta = 1,
                IdExpediente = idExpediente,
                Diagnostico = Request.Form["diagnostico"]

            };

            var json = JsonConvert.SerializeObject(consulta);

            var httpClient = new HttpClient();

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
            HttpResponseMessage response = await httpClient.PostAsync("api/Consulta/", httpContent);

            TempData["ExpedienteUsuario"] = idExpediente;

            if (response.IsSuccessStatusCode)
                TempData["Mensaje"] = "Consulta agregada con éxito.";

            else
                TempData["ErrorMensaje"] = "No se ha podido agregar la consulta.";

            return RedirectToAction("Consultas");

        }

    }
}

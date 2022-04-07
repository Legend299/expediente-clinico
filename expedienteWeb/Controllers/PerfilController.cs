using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using expediente_clinico.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http.Headers;
using System.Text;

namespace expedienteWeb.Controllers
{
    public class PerfilController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Cuenta(Expediente expediente) 
        {
            return View(expediente);
        }

        public async Task<IActionResult> CrearUsuario() {
            Console.WriteLine("CORREO: "+Request.Form["correo"]);
            Console.WriteLine("PASS1: " + Request.Form["contrasena1"]);
            Console.WriteLine("PASS2: " + Request.Form["contrasena2"]);
            Console.WriteLine("CURP: " + Request.Form["curp"]);

            Usuario usuario = new Usuario();
            usuario.Correo = Request.Form["correo"];
            usuario.Contrasena = Request.Form["contrasena1"];
            usuario.Curp = Request.Form["curp"];
            usuario.IdRol = 3;
            
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
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> AccederUsuario() 
        {
            Usuario usuario = new Usuario();

            var httpClient = new HttpClient();
            var json = "";
            //var json = await httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Usuario");
            if (httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Usuario").IsCompleted)
            {
                json = await httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Usuario");
            }
            else
            {
                json = await httpClient.GetStringAsync("http://192.168.1.69:8891/api/Usuario");
            }
            var usuarioList = JsonConvert.DeserializeObject<List<Usuario>>(json);

            string correoForm = Request.Form["correo"];
            string contraForm = Request.Form["contrasena1"];

            foreach (Usuario user in usuarioList)
            {
                if (user.Correo.Equals(correoForm) && user.Contrasena.Equals(contraForm)) 
                {
                    usuario = user;
                    HttpContext.Session.SetString("Id", Convert.ToString(usuario.IdUsuario));
                    HttpContext.Session.SetString("Correo", usuario.Correo);
                    HttpContext.Session.SetString("Curp", usuario.Curp);
                    HttpContext.Session.SetString("Expediente", Convert.ToString(usuario.IdExpediente));
                    return RedirectToAction("Index","Inicio");
                }
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> SolicitarExpediente() 
        {
            if (HttpContext.Session.GetString("Curp") != null) 
            {
                //Expediente expediente = new Expediente();

                var httpClient = new HttpClient();
                var json = "";
                
                if (httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Expediente/"+ HttpContext.Session.GetString("Curp")).IsCompleted)
                {
                    json = await httpClient.GetStringAsync("http://legend.zapto.org:8891/api/Expediente/" + HttpContext.Session.GetString("Curp"));
                }
                else
                {
                    json = await httpClient.GetStringAsync("http://192.168.1.69:8891/api/Expediente/" + HttpContext.Session.GetString("Curp"));
                }
                Expediente expediente = JsonConvert.DeserializeObject<Expediente>(json);

                if (expediente.Curp == null) 
                {
                    Console.WriteLine("SALIO NULO");
                    return RedirectToAction("Index","Inicio");
                }
                if (expediente.Curp.Equals(HttpContext.Session.GetString("Curp")))
                {
                    Console.WriteLine("TIENE EXPEDIENTE");
                    return RedirectToAction("Index","Expediente", expediente);
                }
                else {
                    Console.WriteLine("NO TIENE EXPEDIENTE");
                    return RedirectToAction("Index", "Inicio");
                }
            }
            Console.WriteLine("NO ESTÁ LOGEADO");
            return RedirectToAction("Index","Inicio");
        }

        public void ActualizarExpediente() 
        {
            /*
             
            Conectarse api
             
             */
        }

        public IActionResult CrearCuenta() 
        {
            return View();
        }
    }
}

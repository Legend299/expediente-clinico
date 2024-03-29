﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IHttpClientFactory _httpClientFactory;

        private int _idExpediente = 0;
        public PerfilController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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

            string correoForm = Request.Form["correo"];
            string contraForm = Request.Form["contrasena1"];

            if (correoForm.Trim().Length <= 0 && contraForm.Trim().Length <= 0) {
                TempData["Message"] = "Rellena todos los campos";
                return RedirectToAction("InicioSesion");
            }

            if (correoForm.Trim().Length <= 0) {
                TempData["Message"] = "Ingresa un correo";
                return RedirectToAction("InicioSesion");
            }

            if (contraForm.Trim().Length <= 0) {
                TempData["Message"] = "Ingresa una contraseña";
                TempData["Correo"] = correoForm;
                return RedirectToAction("InicioSesion");
            }
                
            

            // Encriptado de contraseña
            string sha256 = Encrypt.GetSHA256(contraForm);;

            Usuario usuario = new Usuario
            {
                Correo = correoForm,
                Password = sha256
            };

            var httpClient = _httpClientFactory.CreateClient("api");
  
            var json = JsonConvert.SerializeObject(usuario);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
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

            string correo = Request.Form["Correo"];

            if (Request.Form["Correo"].Equals(""))
            {
                TempData["Mensaje"] = "Ingresa un correo";
                return RedirectToAction("Registrar");
            }

            if (Request.Form["contrasena2"].Equals(""))
            {
                TempData["Mensaje"] = "Llena todos los campos";
                TempData["Correo"] = correo;
                return RedirectToAction("Registrar");
            }

            if (Request.Form["contrasena1"].Equals(""))
            {
                TempData["Mensaje"] = "Ingresa una contraseña";
                TempData["Correo"] = correo;
                return RedirectToAction("Registrar");
            }

            if (Request.Form["contrasena1"] != Request.Form["contrasena2"])
            {
                TempData["Mensaje"] = "Las contraseñas no coinciden";

                //string correo = Request.Form["correo"];
                TempData["Correo"] = correo;
                return RedirectToAction("Registrar");
            }

            Usuario usuario = new Usuario();
            usuario.Correo = Request.Form["correo"];
            usuario.Password = Request.Form["contrasena1"];
            usuario.IdRol = 3; //Usuario normal 2 Medico 1 Admin
            usuario.Activo = true;

            // Encriptado de contraseña

            string sha256 = Encrypt.GetSHA256(usuario.Password);
            usuario.Password = sha256;

            var httpClient = _httpClientFactory.CreateClient("api");

            var json = JsonConvert.SerializeObject(usuario);

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Usuario", httpContent);
            Console.WriteLine("CODIGO: " + response);
            Console.WriteLine("Objeto enviado [EXpediente]: "+usuario.IdExpediente);
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

            var httpClient = _httpClientFactory.CreateClient("api");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));
            json = await httpClient.GetStringAsync("api/Expediente/" + Convert.ToString(_idExpediente));

            ExpedienteDTO expedienteUsuario = JsonConvert.DeserializeObject<ExpedienteDTO>(json);

            return expedienteUsuario;
        }
    }
}

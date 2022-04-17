﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using webservice1.Models;
using webservice1.Models.DTO;

namespace ClienteWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;

        public HomeController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        public async Task<IActionResult> Index()
        {
            var json = "";
            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/2").IsCompleted) 
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Expediente/2");
                else
                json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada+"/Expediente/2");
                
            }
            List<ExpedienteDTO> expediente  = JsonConvert.DeserializeObject<List<ExpedienteDTO>>(json);
            return View(expediente);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
using expediente_clinico.Models;
using expediente_clinico.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteController : ControllerBase
    {
        private static readonly ExpedienteRepository expedienteRepository = new ExpedienteRepository();
        [HttpGet]
        public void getListaExpediente() { 
            
        }

        // GET api/Expediente/abc
        [HttpGet("{curp}")]
        public async Task<Expediente> GetExpedientePorCurp(string curp)
        {
            return await expedienteRepository.obtenerExpedientePorCurp(curp);
        }
    }
}

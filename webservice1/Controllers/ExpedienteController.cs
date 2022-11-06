using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpedienteController : ControllerBase
    {
        private readonly IExpedienteService _repository;
        public ExpedienteController(IExpedienteService repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpedienteDTO>> GetExpedienteUsuario(int id)
        {
            var expediente = await _repository.ListarExpedienteUsuario(id);
            if (expediente == null)
                return BadRequest("Expediente no encontrado");
            return Ok(expediente);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Expediente>> GetExpediente(int id) 
        {
            var expediente = await _repository.ListarExpediente(id);
            if (expediente == null)
                return BadRequest("Expediente no encontrado");
            return Ok(expediente);
        }

        [HttpPost]
        public ActionResult<Expediente> PostExpediente(Expediente expediente) 
        {
            Expediente ex = _repository.AgregarExpediente(expediente);
            return Ok(ex);
        }

        [HttpPut]
        public ActionResult<Expediente> PutExpediente(Expediente expediente) 
        {
            var expedienteModificado = _repository.ModificarExpediente(expediente);
            return Ok(expedienteModificado);
        }
    }
}

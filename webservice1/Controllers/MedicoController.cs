using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepository _repository;
        public MedicoController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medico>> GetMedico(int id)
        {
            var medico = await _repository.ObtenerMedicoId(id);
            if (medico == null)
                return BadRequest("Usuario no encontrado");
            return Ok(medico);
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
            var medicoFinal = await _repository.AgregarMedico(medico);
            return Ok(medicoFinal);
        }

    }
}

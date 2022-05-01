using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _repository;
        public MedicoController(IMedicoService repository)
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

        [HttpGet]
        public async Task<ActionResult<List<Medico>>> GetListaMedico() 
        {
            var medico = await _repository.ListarCedula();
            if (medico == null)
                return BadRequest();

            return Ok(medico);
        }

        [HttpGet("Especialidades")]
        public async Task<ActionResult<List<Especialidade>>> GetListaEspecialidades() 
        {
            var especialidades = await _repository.ListaEspecialidades();
            if (especialidades == null)
                return BadRequest();

            return Ok(especialidades);
        }

        [HttpPut]
        public async Task<ActionResult<Medico>> PutMedico(Medico medico) 
        {
            var _medico = await _repository.ModificarMedico(medico);
            if (_medico == null)
                return BadRequest();

            return Ok(_medico);
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
            var medicoFinal = await _repository.AgregarMedico(medico);
            return Ok(medicoFinal);
        }

    }
}

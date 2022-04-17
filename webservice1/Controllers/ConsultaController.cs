using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepository _repository;
        public ConsultaController(IConsultaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Consulta>>> GetConsultaUsuario(int id)
        {
            var listaConsulta = await _repository.ListarConsultasUsuario(id);
            if (listaConsulta == null)
                return BadRequest("Usuario sin consultas");
            return Ok(listaConsulta);
        }

    }
}

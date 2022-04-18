using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;

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
        public async Task<ActionResult<List<webservice1.Data.Consulta>>> GetConsultaUsuario(int id)
        {
            var listaConsulta = await _repository.ListarConsultasUsuario(id);
            if (listaConsulta == null)
                return BadRequest("Usuario sin consultas");
            return Ok(listaConsulta);
        }

        [HttpGet("prueba")]
        public async Task<ActionResult<List<Consulta>>> GetConsulta() {
            return Ok(_repository.ListarConsultas());
        }

        [HttpPost]
        public ActionResult<webservice1.Data.Consulta> PostConsulta(webservice1.Data.Consulta consulta) {
            return Ok(_repository.AgregarConsulta(consulta));
        }

    }
}

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
        public async Task<ActionResult<List<Consulta>>> GetConsultaUsuario(int id)
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
        public ActionResult<Consulta> PostConsulta(Consulta consulta) {
            return Ok(_repository.AgregarConsulta(consulta));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteConsulta(int id) {

            var respuesta = _repository.EliminarConsulta(id);
            if (respuesta)
                return Ok("Se ha eliminado la consulta");

            return BadRequest("No se ha podido eliminar la consulta");
        }

    }
}

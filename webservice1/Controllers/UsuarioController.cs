using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioController(IUsuarioRepository repository) 
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetListaUsuarios() 
        {
            return Ok(await _repository.ListarUsuarios());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id) 
        {
            var usuario = await _repository.ListarUsuario(id);
            if (usuario == null)
                return BadRequest("Usuario no encontrado");
            return Ok(usuario);
        }

        [HttpGet("estados")]
        public async Task<ActionResult<List<Estado>>> GetListaEstados() {
            return Ok(await _repository.ListarEstados());
        }

        [HttpGet("municipios/{idEstado}")]
        public async Task<ActionResult<List<MunicipioDTO>>> GetListaMunicipios(int idEstado) {
            return Ok(await _repository.ListarMunicipios(idEstado));
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            var usuarioFinal = await _repository.AgregarUsuario(usuario);
            return Ok(usuarioFinal);
        }

        [HttpPut]
        public ActionResult PutUsuario(Usuario usuario) 
        {
            _repository.ModificarUsuario(usuario);
            return Ok("Usuario Modificado");
        }
    }
}

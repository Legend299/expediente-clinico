using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _repository;
        public UsuarioController(IUsuarioService repository) 
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Usuario>>> GetListaUsuarios() 
        {
            return Ok(await _repository.ListarUsuarios());
        }

        [HttpGet("{id}")]
        [Authorize]
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
        [Authorize]
        public ActionResult PutUsuario(Usuario usuario) 
        {
            _repository.ModificarUsuario(usuario);
            return Ok("Usuario Modificado");
        }

        // GET Paciente
        [HttpGet("Permiso/{id}")]
        [Authorize]
        public async Task<ActionResult<ExpedientesPermiso>> GetPermisoExpediente(int id) 
        {
            var _permiso = await _repository.ObtenerPermisoMedicoExpediente(id);
            if (_permiso == null)
                return BadRequest();

            return Ok(_permiso);
        }


        // GET Médico
        [HttpGet("Permiso/medico/{id}")]
        [Authorize]
        public async Task<ActionResult<ExpedientesPermiso>> GetPermisoMedico(int id)
        {
            var _permiso = await _repository.ObtenerPermisoMedico(id);
            if (_permiso == null)
                return BadRequest();

            return Ok(_permiso);
        }

        [HttpGet("Permiso/medico/pacientes/{id}")]
        [Authorize]
        public async Task<ActionResult<List<ExpedientesPermiso>>> GetListaPacientes(int id) 
        {
            var _listaPermiso = await _repository.ObtenerListaPacientes(id);
            if (_listaPermiso == null)
                return BadRequest();

            return Ok(_listaPermiso);
        }

        [HttpPut("Permiso")]
        [Authorize]
        public async Task<ActionResult<ExpedientesPermiso>> PutPermiso(ExpedientesPermiso permiso) 
        {
            var _permiso = await _repository.ModificarPermisoMedico(permiso);
            if (_permiso == null)
                return BadRequest();

            return Ok(_permiso);
        }

        [HttpPost("Permiso")]
        [Authorize]
        public async Task<ActionResult<ExpedientesPermiso>> PostPermiso(ExpedientesPermiso permiso) 
        {
            var _permiso = await _repository.CrearPermisoMedico(permiso);
            if (_permiso == null)
                return BadRequest();

            return Ok(_permiso);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioToken>> Login(Usuario usuario) 
        {
            var _cuenta = await _repository.Login(usuario);
            if (_cuenta == null)
                return BadRequest("No existe esa cuenta");

            return Ok(_cuenta);
        }

    }
}

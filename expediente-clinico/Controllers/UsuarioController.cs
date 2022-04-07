using expediente_clinico.Models;
using expediente_clinico.RabbitMQ;
using expediente_clinico.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace expediente_clinico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private static readonly UsuarioRepository usuarioRepository = new UsuarioRepository();

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetListaUsuarios()
        {
            return await usuarioRepository.listarUsuarios();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post(Usuario value)
        {
            //Usuario usuario = JsonConvert.DeserializeObject<Usuario>(value);
            usuarioRepository.agregarUsuario(value);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webservice2.RabbitMQ.Consumidor;

namespace webservice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IConsumidor _recibirMensaje;
        public DocumentoController(IConsumidor recibirMensaje)
        {
            _recibirMensaje = recibirMensaje;

        }

        [HttpGet("recibir")]
        public async Task<ActionResult> GetArchivo()
        {
            _recibirMensaje.RecibirMensaje();
            Console.WriteLine("API 2 CONTROLLER DOCUMENTO");
            return Ok();
        }
    }
}

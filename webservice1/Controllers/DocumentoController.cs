using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;
using webservice1.RabbitMQ;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoRepository _repository;
        private readonly IProductor _publicarMensaje;
        public DocumentoController(IDocumentoRepository repository, IProductor publicarMensaje)
        {
            _repository = repository;
            _publicarMensaje = publicarMensaje;
        }

        [HttpPost]
        public async Task<ActionResult> SubirDocumento([FromForm] DocumentoDTO documento)
        {
            Console.WriteLine();
            bool recibido = _repository.Subir(documento);

            if (recibido)
            {
                _publicarMensaje.MandarMensaje(documento.archivo);
                using (var httpClient = new HttpClient())
                {
                    //if (httpClient.GetStringAsync("http://192.168.1.69:8892/WeatherForecast").IsCompleted)
                        await httpClient.GetStringAsync("http://192.168.1.69:8892/api/documento/recibir");
                    //else
                        //await httpClient.GetStringAsync("http://legend.zapto.org:8892/api/documento/recibir");
                }
                Console.WriteLine("API 1 DOCUMENTO");
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        public ActionResult ObtenerListadocumentos() 
        {
            return Ok("");
        }


    }
}

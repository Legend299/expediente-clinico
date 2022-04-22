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
        public ActionResult SubirDocumento([FromForm] DocumentoDTO documento)
        {
            //Console.WriteLine();
            //bool recibido = _repository.Subir(documento);

            //if (recibido)
            //{
            //    Console.WriteLine("SUBIENDO >>");
            //    bool subiendo = await _publicarMensaje.MandarMensaje(documento.archivo);
            //    if (subiendo)
            //    {
            //        Console.WriteLine("SUBIDO [X]");
            //        using (var httpClient = new HttpClient())
            //        {
            //            //if (httpClient.GetStringAsync("http://192.168.1.69:8892/WeatherForecast").IsCompleted)
            //            await httpClient.GetStringAsync("http://192.168.1.69:8892/api/documento/recibir");
            //            //else
            //            //await httpClient.GetStringAsync("http://legend.zapto.org:8892/api/documento/recibir");
            //        }
            //    }
            //    Console.WriteLine("API 1 DOCUMENTO");
            //    return Ok();
            //}

            //return BadRequest();

            ////string idUsuario = Convert.ToString(documento.IdExpediente);
            ////string archivos = "C:/Users/acer/Desktop/Archivos_Expediente/"+idUsuario;

            ////if (!Directory.Exists(archivos)) 
            ////{
            ////    Directory.CreateDirectory(archivos);
            ////}

            ////string rutaArchivo = Path.Combine(archivos, documento.archivo.FileName);

            ////try
            ////{
            ////    using (FileStream newFile = System.IO.File.Create(rutaArchivo))
            ////    {
            ////        documento.archivo.CopyTo(newFile);
            ////        newFile.Flush();
            ////    }



            ////    return Ok();
            ////}
            ////catch (Exception e)
            ////{
            ////    return BadRequest();
            ////}
            ///
            bool resultado = _repository.Subir(documento);

            if (resultado)
                return Ok();
            return BadRequest();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Documento>>> ObtenerListadocumentosUsuario(int id)
        {
            return Ok(await _repository.ObtenerListadocumentosUsuario(id));
        }

        [HttpGet("Archivo/{id}")]
        public async Task<FileContentResult> ObtenerArchivoPorId(int id) 
        {
            var documento = await _repository.ObtenerArchivo(id);

            return File(System.IO.File.ReadAllBytes(documento.Ruta), "application/octet-stream", documento.Nombre);
        }


    }
}

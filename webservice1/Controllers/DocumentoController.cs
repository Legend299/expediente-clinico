using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using webservice1.Models.DTO;
using webservice1.RabbitMQ;
using webservice2.Models;

namespace webservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoService _repository;
        private readonly IProductor _publicarMensaje;
        public DocumentoController(IDocumentoService repository, IProductor publicarMensaje)
        {
            _repository = repository;
            _publicarMensaje = publicarMensaje;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SubirDocumento([FromForm] DocumentoDTO documento)
        {

            bool resultado = await _repository.SubirAzure(documento);

            DocumentoInfo documentoInfo = new DocumentoInfo
            {
                Nombre = documento.Nombre,
                Extension = documento.Extension,
                Medico = documento.Medico,
                Peso = documento.Peso,
                IdExpediente = documento.IdExpediente
            };

            bool res = await _publicarMensaje.MandarMensaje(documentoInfo);
            /*
             *  Prueba private
             */
            if (res)
            {
                try
                {
                    if (documento.Archivo != null && documento.Archivo.Length > 0)
                    {
                        using (var client = new HttpClient())
                        {
                            try
                            {
                                client.BaseAddress = new Uri("http://192.168.1.69:8894/api");
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", documento.Token);
                                byte[] data;
                                using (var br = new BinaryReader(documento.Archivo.OpenReadStream()))
                                    data = br.ReadBytes((int)documento.Archivo.OpenReadStream().Length);

                                ByteArrayContent bytes = new ByteArrayContent(data);


                                MultipartFormDataContent multiContent = new MultipartFormDataContent();

                                multiContent.Add(bytes, "file", documento.Archivo.FileName);

                                var result = client.PostAsync("api/Documento", multiContent).Result;

                                return StatusCode((int)result.StatusCode); //201 Created the request has been fulfilled, resulting in the creation of a new resource.

                            }
                            catch (Exception)
                            {
                                return StatusCode(500); // 500 is generic server error
                            }
                        }
                    }

                    return StatusCode(400); // 400 is bad request

                }
                catch (Exception)
                {
                    return StatusCode(500); // 500 is generic server error
                }
            }

            /*
             * Prueba private
             */

            return Ok("Archivo subido");

        }

        [HttpGet("{id}")]
        [Authorize]
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

        [HttpGet("ArchivoAzure/{id}")]
        public async Task<FileContentResult> ObtenerArchivoAzurePorId(int id)
        {
            var doc = await _repository.ObtenerArchivoAzure(id);
            return File(doc.Contenido, "application/octet-stream", doc.Nombre);
        }

        [HttpDelete("ArchivoAzure/{id}")]
        [Authorize]
        public ActionResult EliminarArchivoAzure(int id) 
        {
            var res = _repository.EliminarArchivoAzure(id);
            return Ok();
        }

        //[HttpGet("ArchivoAzure/{id}")]
        //public async Task<byte[]> ObtenerArchivoAzurePorId(int id)
        //{
        //    var doc = await _repository.ObtenerArchivoAzure(id);
        //    return doc.Contenido;
        //}


    }
}

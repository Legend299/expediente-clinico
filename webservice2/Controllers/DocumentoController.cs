using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webservice2.RabbitMQ.Consumidor;

namespace webservice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentoController : ControllerBase
    {
        private readonly IConsumidor _recibirMensaje;
        public DocumentoController(IConsumidor recibirMensaje)
        {
            _recibirMensaje = recibirMensaje;

        }

        [HttpPost]
        public async Task<ActionResult> PostArchivo()
        {

            /*
             * Private
             */

            if (Request.HasFormContentType)
            {
                var form = Request.Form;
                foreach (var formFile in form.Files)
                {
                    string idUsuario = Convert.ToString(7);
                    string archivos = "C:/Users/acer/Desktop/Test_Archivos/9999/" + idUsuario;

                    
                    if (!Directory.Exists(archivos))
                    {
                        Directory.CreateDirectory(archivos);
                    }

                    string rutaArchivo = Path.Combine(archivos, formFile.FileName);

                    await _recibirMensaje.RecibirMensaje(rutaArchivo);

                    try
                    {

                        using (FileStream newFile = System.IO.File.Create(rutaArchivo))
                        {
                            formFile.CopyTo(newFile);
                            newFile.Flush();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                }
            }

            /*
             * Private
             */

            return Ok();
        }

        [HttpGet]
        public ActionResult GetNotification() 
        {
            return Ok("Api Corriendo");
        }
    }
}

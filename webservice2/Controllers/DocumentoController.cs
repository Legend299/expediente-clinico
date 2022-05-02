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
                    string idUsuario = Convert.ToString(111);
                    string archivos = "C:/Users/acer/Desktop/Test_Archivos/135/" + idUsuario;

                    if (!Directory.Exists(archivos))
                    {
                        Directory.CreateDirectory(archivos);
                    }

                    string rutaArchivo = Path.Combine(archivos, formFile.FileName);

                    try
                    {

                        using (FileStream newFile = System.IO.File.Create(rutaArchivo))
                        {
                            formFile.CopyTo(newFile);
                            newFile.Flush();
                        }

                        _recibirMensaje.RecibirMensaje(rutaArchivo);

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

            ////_recibirMensaje.RecibirMensaje();

            //string idUsuario = Convert.ToString(111);
            //string archivos = "C:/Users/acer/Desktop/Test_Archivos/"+idUsuario;

            //if (!Directory.Exists(archivos))
            //{
            //    Directory.CreateDirectory(archivos);
            //}

            //string rutaArchivo = Path.Combine(archivos, archivo.FileName);

            //try
            //{

            //    using (FileStream newFile = System.IO.File.Create(rutaArchivo))
            //    {
            //        archivo.CopyTo(newFile);
            //        newFile.Flush();
            //    }

            //    _recibirMensaje.RecibirMensaje(rutaArchivo);

            //}
            //catch (Exception e) 
            //{
            //    throw new Exception(e.Message);
            //}

            return Ok();
        }

        [HttpGet]
        public ActionResult GetNotification() 
        {
            return Ok("Api Corriendo");
        }
    }
}

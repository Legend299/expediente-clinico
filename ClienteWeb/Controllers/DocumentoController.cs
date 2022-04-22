using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using webservice1.Models.DTO;

namespace ClienteWeb.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;

        public DocumentoController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        public IActionResult Ver()
        {
            return View();
        }

        /*
         * 
         */
        public async Task<IActionResult> SubirDocumento(IFormFile archivo) 
        {
            //DocumentoDTO documento = new DocumentoDTO();
            //documento.Nombre = archivo.FileName;
            //documento.Extension = System.IO.Path.GetExtension(archivo.FileName);
            //documento.Medico = "nombre medico";
            //documento.Peso = Convert.ToInt32(archivo.Length/1024);
            //documento.IdExpediente = (int)HttpContext.Session.GetInt32("Expediente");

            //documento.archivo = archivo;


            /*
             * No headers
             */
            //var client = new HttpClient
            //{
            //    BaseAddress = new(_conexionApi.Value.conexionPrivada)
            //};

            //await using var stream = archivo.OpenReadStream();

            //using var request = new HttpRequestMessage(HttpMethod.Post, "api/Documento");

            //var payload = new
            //{
            //    Nombre = archivo.FileName,
            //    Extension = System.IO.Path.GetExtension(archivo.FileName),
            //    Medico = "nombre medico",
            //    Peso = Convert.ToInt32(archivo.Length / 1024),
            //    IdExpediente = (int)HttpContext.Session.GetInt32("Expediente")
            //};

            //using var content = new MultipartFormDataContent
            //{

            //    // payload
            //    { new StringContent(payload.Nombre), "DocumentoDTO.Nombre" },
            //    { new StringContent(payload.Extension), "DocumentoDTO.Extension" },
            //    { new StringContent(payload.Medico), "DocumentoDTO.Medico" },
            //    { new StringContent(Convert.ToString(payload.Peso)), "DocumentoDTO.Peso" },
            //    { new StringContent(Convert.ToString(payload.IdExpediente)), "DocumentoDTO.IdExpediente" },

            //    // file
            //    { new StreamContent(stream), "archivo", archivo.FileName }
            //};

            //request.Content = content;

            //HttpResponseMessage response = await client.SendAsync(request);

            //Console.WriteLine("CODIGO ARCHIVO: \n"+response);

            var cliente = new HttpClient();

            using (var multipartFormContent = new MultipartFormDataContent()) {
                multipartFormContent.Add(new StringContent(archivo.FileName), name: "Nombre");
                multipartFormContent.Add(new StringContent(System.IO.Path.GetExtension(archivo.FileName)), name: "Extension");
                multipartFormContent.Add(new StringContent("Medico Ejemplo"), name: "Medico");
                multipartFormContent.Add(new StringContent(Convert.ToString(archivo.Length)), name: "Peso");
                multipartFormContent.Add(new StringContent(Convert.ToString(1)), name: "IdExpediente");

                var filestreamContent = new StreamContent(archivo.OpenReadStream());
                filestreamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);

                multipartFormContent.Add(filestreamContent, name: "Archivo", fileName: archivo.FileName);

                var response = await cliente.PostAsync(_conexionApi.Value.conexionPrivada+ "/Documento",multipartFormContent);
                var test = await response.Content.ReadAsStringAsync();
                // Código
                Console.WriteLine(test);
            }

                return RedirectToAction("Ver");

        }

    }
}

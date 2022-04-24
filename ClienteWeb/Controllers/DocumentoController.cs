using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using webservice1.Models;
using webservice1.Models.DTO;

namespace ClienteWeb.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly IOptions<ConexionApi> _conexionApi;

        private int _idExpediente = 0;

        public DocumentoController(IOptions<ConexionApi> conexionApi)
        {
            _conexionApi = conexionApi;
        }

        public async Task<IActionResult> Ver()
        {
            _idExpediente = (int)HttpContext.Session.GetInt32("Expediente");
            List<Documento> listaDocumentos = await SolicitarListaDocumentos();
            return View(listaDocumentos);
        }

        public IActionResult Subir() 
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

            //if (archivo == null)
            //    Ver();

            try
            {
                var cliente = new HttpClient();

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    multipartFormContent.Add(new StringContent(archivo.FileName), name: "Nombre");
                    multipartFormContent.Add(new StringContent(System.IO.Path.GetExtension(archivo.FileName)), name: "Extension");
                    multipartFormContent.Add(new StringContent(HttpContext.Session.GetString("Nombre")), name: "Medico");
                    multipartFormContent.Add(new StringContent(Convert.ToString(archivo.Length)), name: "Peso");
                    multipartFormContent.Add(new StringContent(Convert.ToString((int)HttpContext.Session.GetInt32("Expediente"))), name: "IdExpediente");

                    var filestreamContent = new StreamContent(archivo.OpenReadStream());
                    filestreamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);

                    multipartFormContent.Add(filestreamContent, name: "Archivo", fileName: archivo.FileName);

                    var response = await cliente.PostAsync(_conexionApi.Value.conexionPrivada + "/Documento", multipartFormContent);
                    //var test = await response.Content.ReadAsStringAsync();
                    // Código
                    //Console.WriteLine(test);
                }
            TempData["Mensaje"] = "Se ha subido: "+archivo.FileName;
            return RedirectToAction("Ver");
            }
            catch (Exception e) 
            {
                TempData["ErrorMensaje"] = "No se ha podido subir el archivo.";
                return RedirectToAction("Ver");
            }
  
        }

        //public async Task<FileResult> DescargarDocumento(int IdDocumento, string FileName) 
        //{
        //    byte[] json;
        //    using (var httpClient = new HttpClient())
        //    {
        //        if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Documento/" + 1).IsCompleted)
        //            json = await httpClient.GetByteArrayAsync(_conexionApi.Value.conexionPublica + "/Documento/Archivo/" + Convert.ToString(IdDocumento));
        //        else
        //            json = await httpClient.GetByteArrayAsync(_conexionApi.Value.conexionPrivada + "/Documento/Archivo/" + Convert.ToString(IdDocumento));
        //    }

        //    //return json;

        //    //List<Documento> listaDocumento = JsonConvert.DeserializeObject<List<Documento>>(json);

        //    //string path = "C:/Users/acer/Desktop/Archivos_Expediente/13/71d1bcb6f4efa9bc6702c521d88808a3.jpg";
        //    //byte[] bytes = System.IO.File.ReadAllBytes(path);
        //    //return File(bytes, "application/octet-stream", FileName);
        //    return File(json, "application/octet-stream", FileName);
        //}

        //public async void DescargarDocumento(int IdDocumento, string FileName) 
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        await httpClient.GetAsync(_conexionApi.Value.conexionPrivada + "/Documento/Archivo/" + Convert.ToString(IdDocumento));
        //    }
        //}

        public async Task<List<Documento>> SolicitarListaDocumentos()
        {
            int idExpediente = _idExpediente;
            var json = "";
            using (var httpClient = new HttpClient())
            {
                if (httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Documento/" + Convert.ToString(idExpediente)).IsCompleted)
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPublica + "/Documento/" + Convert.ToString(idExpediente));
                else
                    json = await httpClient.GetStringAsync(_conexionApi.Value.conexionPrivada + "/Documento/" + Convert.ToString(idExpediente));

            }

            List<Documento> listaDocumento = JsonConvert.DeserializeObject<List<Documento>>(json);

            return listaDocumento;
        }

    }
}

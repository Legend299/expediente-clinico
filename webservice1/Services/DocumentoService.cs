using Azure.Storage.Blobs;
using ClienteWeb.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;

namespace webservice1.Repository
{
    public class DocumentoService : IDocumentoService
    {
        private readonly expedienteContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        public DocumentoService(expedienteContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }
        public bool Subir(DocumentoDTO documento)
        {

            string idUsuario = Convert.ToString(documento.IdExpediente);
            string archivos = "C:/Users/acer/Desktop/Archivos_Expediente/" + idUsuario;

            if (!Directory.Exists(archivos))
            {
                Directory.CreateDirectory(archivos);
            }

            string rutaArchivo = Path.Combine(archivos, documento.Archivo.FileName);

            try
            {
                using (FileStream newFile = System.IO.File.Create(rutaArchivo))
                {
                    documento.Archivo.CopyTo(newFile);
                    newFile.Flush();
                }

                Documento doc = new Documento
                {
                    Nombre = documento.Nombre,
                    Extension = documento.Extension,
                    Ruta = rutaArchivo,
                    Medico = documento.Medico,
                    Peso = documento.Peso,
                    IdExpediente = documento.IdExpediente
                };

                _context.Documentos.Add(doc);
                _context.SaveChanges();



                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error --> " + e.Message);
                return false;
            }
        }

        public async Task<bool> SubirAzure(DocumentoDTO documento)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Azure blob Guardar Archivo");
            Console.WriteLine("-----------------------------");
            /*
            * Azure blob Guardar Archivo
            */
            try
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient("docusuarios");

                var blobClient = blobContainer.GetBlobClient(documento.IdExpediente + "/" + documento.Archivo.FileName);

                blobClient.DeleteIfExists();

                await blobClient.UploadAsync(documento.Archivo.OpenReadStream());

                string rutaArchivo = blobClient.Uri.AbsoluteUri;

                //string rutaArchivo = documento.IdExpediente + "/" + documento.Archivo.FileName;

                Documento doc = new Documento
                {
                    Nombre = documento.Nombre,
                    Extension = documento.Extension,
                    Ruta = rutaArchivo,
                    Medico = documento.Medico,
                    Peso = documento.Peso,
                    IdExpediente = documento.IdExpediente
                };

                _context.Documentos.Add(doc);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
            /*
             * Azure blob Guardar Archivo
             */
        }

        public async Task<List<Documento>> ObtenerListadocumentosUsuario(int idExpediente)
        {
            var listaDocumentos = await _context.Documentos.Where(ide => ide.IdExpediente == idExpediente).ToListAsync();
            return listaDocumentos;
        }

        public async Task<Documento> ObtenerArchivo(int id)
        {
            var documento = await _context.Documentos.Where(d => d.IdDocumento == id).FirstOrDefaultAsync();
            return documento;
        }

        public async Task<DocumentoAzure> ObtenerArchivoAzure(int id) 
        {
            DocumentoAzure documentoAzure = new DocumentoAzure();

            var documento = await _context.Documentos.Where(d => d.IdDocumento == id).FirstOrDefaultAsync();

            // Azure Blob
            var blobContainer = _blobServiceClient.GetBlobContainerClient("docusuarios");

            // Ruta default
            //var blobClient = blobContainer.GetBlobClient(documento.Ruta);

            // Ruta V2
            string ruta = documento.IdExpediente + "/" + documento.Nombre;
            var blobClient = blobContainer.GetBlobClient(ruta);

            var downloadContent = await blobClient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);

                documentoAzure.Nombre = documento.Nombre;
                documentoAzure.Contenido = ms.ToArray();

                //return ms.ToArray();
                return documentoAzure;
            }
        }

        public bool EliminarArchivoAzure(int id) 
        {
            try
            {

                var documento = _context.Documentos.AsNoTracking().Where(x => x.IdDocumento == id).FirstOrDefault();

                if (documento != null)
                {
                    //string rutaDocumento = documento.Ruta;
                    string rutaDocumento = documento.IdExpediente + "/" + documento.Nombre;

                    // Azure Blob
                    var blobContainer = _blobServiceClient.GetBlobContainerClient("docusuarios");
                    var blobClient = blobContainer.GetBlobClient(rutaDocumento);
                    var response = blobClient.Delete();

                    Documento doc = new Documento
                    {
                        IdDocumento = documento.IdDocumento
                    };

                    _context.Documentos.Remove(doc);
                    _context.SaveChanges();

                    return true;
                }

                return false;

            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }


        }
    }
}

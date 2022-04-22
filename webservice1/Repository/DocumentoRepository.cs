using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;

namespace webservice1.Repository
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly expedienteContext _context;
        public DocumentoRepository(expedienteContext context)
        {
            _context = context;
        }
        public bool Subir(DocumentoDTO documento)
        {

            string idUsuario = Convert.ToString(documento.IdExpediente);
            string archivos = "C:/Users/acer/Desktop/Archivos_Expediente/" + idUsuario;

            if (!Directory.Exists(archivos))
            {
                Directory.CreateDirectory(archivos);
            }

            string rutaArchivo = Path.Combine(archivos, documento.archivo.FileName);

            try
            {
                using (FileStream newFile = System.IO.File.Create(rutaArchivo))
                {
                    documento.archivo.CopyTo(newFile);
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
                Console.WriteLine("Error --> "+e.Message);
                return false;
            }
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
    }
}

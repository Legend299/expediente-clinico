using webservice1.Models.DTO;

namespace webservice1.Repository
{
    public class DocumentoRepository : IDocumentoRepository
    {
        public bool Subir(DocumentoDTO documento)
        {
            Console.WriteLine("NOMBRE DOCUMENTO: "+documento.Nombre);
            Console.WriteLine("Documento");
            return true;
        }
    }
}

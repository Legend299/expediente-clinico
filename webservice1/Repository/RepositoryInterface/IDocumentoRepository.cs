using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IDocumentoRepository
    {
        public bool Subir(DocumentoDTO documento);
    }
}

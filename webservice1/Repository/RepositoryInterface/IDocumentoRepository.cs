using Microsoft.AspNetCore.Mvc;
using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IDocumentoRepository
    {
        public bool Subir(DocumentoDTO documento);

        public Task<List<Documento>> ObtenerListadocumentosUsuario(int idExpediente);

        public Task<Documento> ObtenerArchivo(int id);
    }
}

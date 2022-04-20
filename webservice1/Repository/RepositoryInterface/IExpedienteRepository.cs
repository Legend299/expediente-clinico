using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IExpedienteRepository
    {
        public Task<ExpedienteDTO?> ListarExpedienteUsuario(int id);
        public Task<List<Expediente>?> ListarExpediente(int id);
        public Expediente AgregarExpediente(Expediente expediente);
        public Expediente ModificarExpediente(Expediente expediente);
    }
}

using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IConsultaRepository
    {
        public Task<ConsultaDTO?> ListarConsultaUsuario(int idConsulta);
        public Task<List<ConsultaDTO>?> ListarConsultasUsuario(int idExpediente);
        public Task<ConsultaDTO> AgregarExpediente(ConsultaDTO consulta);
        public Task<ConsultaDTO> ModificarExpediente(ConsultaDTO consulta);
    }
}

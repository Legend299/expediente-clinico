using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IConsultaService
    {
        public Task<ConsultaDTO?> ListarConsultaUsuario(int idConsulta);
        public Task<List<ConsultaDTO>?> ListarConsultasUsuario(int idExpediente);
        public Consulta AgregarConsulta(Consulta consulta);
        public Task<ConsultaDTO> ModificarConsulta(ConsultaDTO consulta);

        public bool EliminarConsulta(int id);

        public Task<List<Consulta>?> ListarConsultas();
    }
}

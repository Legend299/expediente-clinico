using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IConsultaRepository
    {
        public Task<ConsultaDTO?> ListarConsultaUsuario(int idConsulta);
        public Task<List<ConsultaDTO>?> ListarConsultasUsuario(int idExpediente);
        public Consulta AgregarConsulta(Consulta consulta);
        public Task<ConsultaDTO> ModificarConsulta(ConsultaDTO consulta);

        public bool EliminarConsulta(Consulta consulta);

        public Task<List<Consulta>?> ListarConsultas();
    }
}

using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IConsultaRepository
    {
        public Task<ConsultaDTO?> ListarConsultaUsuario(int idConsulta);
        public Task<List<ConsultaDTO>?> ListarConsultasUsuario(int idExpediente);
        public webservice1.Data.Consulta AgregarConsulta(webservice1.Data.Consulta consulta);
        //public Consulta AgregarConsulta(Consulta consulta);
        public Task<ConsultaDTO> ModificarConsulta(ConsultaDTO consulta);

        public Task<List<Consulta>?> ListarConsultas();
    }
}

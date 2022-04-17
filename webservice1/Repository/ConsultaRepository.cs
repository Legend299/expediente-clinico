using webservice1.Models.DTO;

namespace webservice1.Repository
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly expedienteContext _context;
        public ConsultaRepository(expedienteContext context)
        {
            _context = context;
        }

        public Task<ConsultaDTO> AgregarExpediente(ConsultaDTO consulta)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ConsultaDTO>?> ListarConsultasUsuario(int idExpediente)
        {
            //var listaConsulta = await _context.Consultas.Where(ide => ide.IdExpediente == idExpediente).ToListAsync();

            var lstConsulta = await _context.Consultas.Where(ide => ide.IdExpediente == idExpediente).Select(c => new ConsultaDTO
            {
                IdConsulta = c.IdConsulta,
                Fecha = c.Fecha,
                Medico = c.Medico,
                IdTipoConsulta = c.IdTipoConsulta,
                Diagnostico = c.Diagnostico,
                IdExpediente = c.IdExpediente
            }).ToListAsync();

            return lstConsulta;
        }

        public Task<ConsultaDTO?> ListarConsultaUsuario(int idConsulta)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultaDTO> ModificarExpediente(ConsultaDTO consulta)
        {
            throw new NotImplementedException();
        }
    }
}

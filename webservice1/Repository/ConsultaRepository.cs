using webservice1.Models.DTO;

namespace webservice1.Repository
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly expedienteContext _context;
        private readonly webservice1.Data.expedienteContext _dbcontext;
        public ConsultaRepository(expedienteContext context, webservice1.Data.expedienteContext dbcontext)
        {
            _context = context;
            _dbcontext = dbcontext;
        }

        public webservice1.Data.Consulta AgregarConsulta(webservice1.Data.Consulta consulta)
        {
            try
            {
                _dbcontext.Consultas.Add(consulta);
                _dbcontext.SaveChanges();
                return consulta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se ha podido agregar la consulta: " + ex.Message);
                return null;
            }
        }

        public async Task<List<ConsultaDTO>?> ListarConsultasUsuario(int idExpediente)
        {
            //var listaConsulta = await _context.Consultas.Where(ide => ide.IdExpediente == idExpediente).ToListAsync();

            var lstConsulta = await _dbcontext.Consultas.Where(ide => ide.IdExpediente == idExpediente).Select(c => new ConsultaDTO
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

        //Prueba Listar consultas General
        public async Task<List<Consulta>?> ListarConsultas() {
            return await _context.Consultas.Where(idm => idm.IdExpediente == 1).ToListAsync();
        }

        public Task<ConsultaDTO?> ListarConsultaUsuario(int idConsulta)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultaDTO> ModificarConsulta(ConsultaDTO consulta)
        {
            throw new NotImplementedException();
        }
    }
}

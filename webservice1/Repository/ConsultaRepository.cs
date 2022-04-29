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

        public Consulta AgregarConsulta(Consulta consulta)
        {
            try
            {
                _context.Consultas.Add(consulta);
                _context.SaveChanges();
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

            var lstConsulta = await _context.Consultas.OrderByDescending(or => or.Fecha).Where(ide => ide.IdExpediente == idExpediente).Select(c => new ConsultaDTO
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

        public bool EliminarConsulta(Consulta consulta)
        {
            try
            {
                _context.Consultas.Remove(consulta);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e) 
            {
                Console.WriteLine("ERROR ELIMINAR CONSULTA: "+e.Message);
                return false;  
            }
            
        }
    }
}

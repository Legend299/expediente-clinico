namespace webservice1.Repository
{
    public class MedicoService : IMedicoService
    {
        private readonly expedienteContext _context;
        public MedicoService(expedienteContext context)
        {
            _context = context;
        }
        public async Task<Medico> AgregarMedico(Medico medico)
        {
            try
            {
                _context.Medicos.Add(medico);
                _context.SaveChanges();

                var medicoFinal = await _context.Medicos.Where(idu => idu.IdUsuario == medico.IdUsuario).FirstOrDefaultAsync();

                return medicoFinal;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se ha podido agregar el médico: " + ex.Message);
                return null;
            }
        }

        public async Task<List<Medico>> ListarCedula()
        {
            var listaMedicos = await _context.Medicos.Where(x => x.IdUsuario == null).ToListAsync();
            return listaMedicos;
        }

        public async Task<Medico> ModificarMedico(Medico medico)
        {
            _context.Medicos.Update(medico);
            _context.SaveChanges();

            var _medico = await _context.Medicos.Where(x => x.IdUsuario == medico.IdUsuario).FirstOrDefaultAsync();
            return _medico;
        }

        public async Task<Medico?> ObtenerMedicoId(int id)
        {
            return await _context.Medicos.Where(c =>
                 c.IdUsuario == id)
                     .FirstOrDefaultAsync();
        }

        public async Task<List<Especialidade>> ListaEspecialidades()
        {
            return await _context.Especialidades.ToListAsync();
        }
    }
}

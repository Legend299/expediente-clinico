namespace webservice1.Repository
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly expedienteContext _context;
        public MedicoRepository(expedienteContext context)
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

        public Task<Medico> ModificarMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public async Task<Medico?> ObtenerMedicoId(int id)
        {
            return await _context.Medicos.Where(c =>
                 c.IdUsuario == id)
                     .FirstOrDefaultAsync();
        }
    }
}

namespace webservice1.Repository.RepositoryInterface
{
    public interface IMedicoRepository
    {
        public Task<Medico> AgregarMedico(Medico medico);
        public Task<Medico> ModificarMedico(Medico medico);
        public Task<Medico?> ObtenerMedicoId(int id);
        public Task<List<Especialidade>> ListaEspecialidades();
        public Task<List<Medico>> ListarCedula();
    }
}

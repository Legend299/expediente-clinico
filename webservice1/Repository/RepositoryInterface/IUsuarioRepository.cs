using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IUsuarioRepository
    {
        public Task<List<UsuarioDTO>?> ListarUsuarios();
        public Task<Usuario?> ListarUsuario(int id);
        public Task<UsuarioDTO?> AgregarUsuario(Usuario usuario);
        public bool ModificarUsuario(Usuario usuario);
        public Task<List<Estado>> ListarEstados();
    }
}

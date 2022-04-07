using expediente_clinico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.RepositoryInterface
{
    interface IUsuarioRepository
    {
        public Task<List<Usuario>> listarUsuarios();
        public Task<Usuario> listarUsuarioPorId(long id);
        public void actualizarUsuario(Usuario usuario, long id);
        public void borrarUsuario(long id);
        public void agregarUsuario(Usuario usuario);
    }
}

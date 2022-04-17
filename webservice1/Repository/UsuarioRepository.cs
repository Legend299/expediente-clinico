using webservice1.Models.DTO;

namespace webservice1.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly expedienteContext _context;
        public UsuarioRepository(expedienteContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioDTO>?> ListarUsuarios() 
        {
            var listaUsuarios = await _context.Usuarios.Select(u => new UsuarioDTO 
            {
                IdUsuario = u.IdUsuario,
                Correo = u.Correo,
                Password = u.Password,
                IdRol = u.IdRol,
                IdExpediente = u.IdExpediente,
                Activo = u.Activo
            }).ToListAsync();

            foreach (UsuarioDTO user in listaUsuarios) 
            {
                if (!(user.IdExpediente.HasValue)) 
                    user.IdExpediente = 0;
            }
            return listaUsuarios;
        }

        public async Task<Usuario?> ListarUsuario(int id) 
        {
            return await _context.Usuarios.Where(c =>
                c.IdUsuario == id)
                    .FirstOrDefaultAsync();
        }

        public async Task<UsuarioDTO?> AgregarUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                var usuarioFinal = await _context.Usuarios.Where(idc => idc.Correo == usuario.Correo).Select(u => new UsuarioDTO
                {
                    IdUsuario = u.IdUsuario,
                    Correo = u.Correo,
                    Password = u.Password,
                    IdRol = u.IdRol,
                    IdExpediente = u.IdExpediente,
                    Activo = u.Activo
                }).FirstOrDefaultAsync();

                if (usuarioFinal.IdExpediente.HasValue)
                    usuarioFinal.IdExpediente = 0;

                return usuarioFinal;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("No se ha podido agregar el usuario: "+ex.Message);
                return null;
            }

        }

        public bool ModificarUsuario(Usuario usuario)
        {
            try 
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return true;
            } 
            catch (Exception ex) 
            {
                Console.WriteLine("No se ha podido modificar el usuario: "+ex.Message);
                return false;
            }
            
        }
    }
}

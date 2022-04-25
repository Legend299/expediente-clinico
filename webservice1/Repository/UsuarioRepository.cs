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

        public async Task<List<Estado>> ListarEstados() {
            var listaEstados = await _context.Estados.ToListAsync();
            return listaEstados;
        }

        public async Task<List<MunicipioDTO>> ListarMunicipios(int idEstado) {
            var listaMunicipios = await _context.MunicipiosEstados.Where(ide =>
            ide.IdEstado == idEstado).Select(m => new MunicipioDTO
            {
                IdMunicipio = m.IdMunicipio,
                Nombre = _context.Municipios.Where(idm => idm.IdMunicipio == m.IdMunicipio).Select(nombre => nombre.Nombre).FirstOrDefault()
            }).ToListAsync();
           
            return listaMunicipios;
        }

        public async Task<ExpedientesPermiso> CrearPermisoMedico(ExpedientesPermiso permiso) 
        {
            _context.ExpedientesPermisos.Add(permiso);
            _context.SaveChanges();

            var permisoFinal = await _context.ExpedientesPermisos.OrderByDescending(ep => ep.IdPermiso)
                .Where(ep => ep.IdUsuario == permiso.IdUsuario)
                .FirstOrDefaultAsync();

            return permisoFinal;
        }

        public async Task<ExpedientesPermiso> ObtenerPermisoMedicoExpediente(int idExpediente)
        {
            var permiso = await _context.ExpedientesPermisos.OrderByDescending(ep => ep.IdPermiso)
                .Where(ide => ide.IdExpediente == idExpediente)
                .FirstOrDefaultAsync();

            return permiso;
        }

        public async Task<ExpedientesPermiso> ObtenerPermisoMedico(int idMedico) 
        {
            var permiso = await _context.ExpedientesPermisos.OrderByDescending(ep => ep.IdPermiso)
                .Where(ide => ide.IdUsuario == idMedico)
                .FirstOrDefaultAsync();

            return permiso;
        }

        public async Task<List<ExpedientesPermiso>> ObtenerListaPacientes(int idMedico)
        {
            var permiso = await _context.ExpedientesPermisos.OrderByDescending(ep => ep.IdPermiso)
                .Where(ide => ide.IdUsuario == idMedico && ide.Permiso == true)
                .ToListAsync();

            return permiso;
        }

        public async Task<ExpedientesPermiso> ModificarPermisoMedico(ExpedientesPermiso permiso) 
        {
            if (permiso.PermisoMedico == true && permiso.PermisoUsuario == true)
                permiso.Permiso = true;

            _context.ExpedientesPermisos.Update(permiso);
            _context.SaveChanges();

            var permisoFinal = await _context.ExpedientesPermisos.OrderByDescending(ep => ep.IdPermiso)
                .Where(ide => ide.IdExpediente == permiso.IdExpediente)
                .FirstOrDefaultAsync();

            return permisoFinal;

        }
    }
}

﻿using webservice1.Models.DTO;

namespace webservice1.Repository.RepositoryInterface
{
    public interface IUsuarioRepository
    {
        public Task<List<UsuarioDTO>?> ListarUsuarios();
        public Task<Usuario?> ListarUsuario(int id);
        public Task<UsuarioDTO?> AgregarUsuario(Usuario usuario);
        public bool ModificarUsuario(Usuario usuario);
        public Task<List<Estado>> ListarEstados();
        public Task<List<MunicipioDTO>> ListarMunicipios(int idEstado);
        public Task<ExpedientesPermiso> CrearPermisoMedico(ExpedientesPermiso permiso);
        public Task<ExpedientesPermiso> ObtenerPermisoMedico(int idExpediente);
        public Task<ExpedientesPermiso> ObtenerPermisoMedicoExpediente(int idExpediente);
        public Task<List<ExpedientesPermiso>> ObtenerListaPacientes(int idMedico);
        public Task<ExpedientesPermiso> ModificarPermisoMedico(ExpedientesPermiso permiso);
    }
}

namespace webservice1.Models.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public int? IdExpediente { get; set; }
        public bool Activo { get; set; }
    }
}

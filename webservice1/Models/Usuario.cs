using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            ExpedientesPermisos = new HashSet<ExpedientesPermiso>();
            Medicos = new HashSet<Medico>();
        }

        public int IdUsuario { get; set; }
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdRol { get; set; }
        public int? IdExpediente { get; set; }
        public bool Activo { get; set; }

        public virtual Expediente? IdExpedienteNavigation { get; set; }
        public virtual Role? IdRolNavigation { get; set; } = null!;
        public virtual ICollection<ExpedientesPermiso>? ExpedientesPermisos { get; set; }
        public virtual ICollection<Medico>? Medicos { get; set; }
    }
}

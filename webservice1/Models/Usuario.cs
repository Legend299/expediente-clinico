using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Consulta = new HashSet<Consulta>();
            Documentos = new HashSet<Documento>();
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
        public virtual ICollection<Consulta> Consulta { get; set; }
        public virtual ICollection<Documento> Documentos { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}

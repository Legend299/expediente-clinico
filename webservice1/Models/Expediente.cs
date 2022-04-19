using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Expediente
    {
        public Expediente()
        {
            Consulta = new HashSet<Consulta>();
            Documentos = new HashSet<Documento>();
            ExpedientesPermisos = new HashSet<ExpedientesPermiso>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdExpediente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Imagen { get; set; }
        public string Curp { get; set; } = null!;
        public int IdEstado { get; set; }
        public int IdMunicipio { get; set; }
        public bool Sexo { get; set; }
        public string Telefono { get; set; } = null!;
        public DateOnly FechaDeNacimiento { get; set; }

        public virtual Estado? IdEstadoNavigation { get; set; } = null!;
        public virtual Municipio? IdMunicipioNavigation { get; set; } = null!;
        public virtual ICollection<Consulta>? Consulta { get; set; }
        public virtual ICollection<Documento>? Documentos { get; set; }
        public virtual ICollection<ExpedientesPermiso>? ExpedientesPermisos { get; set; }
        public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}

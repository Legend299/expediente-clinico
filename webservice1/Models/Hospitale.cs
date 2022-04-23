using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Hospitale
    {
        public Hospitale()
        {
            Medicos = new HashSet<Medico>();
        }

        public int IdHospital { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdEstado { get; set; }
        public int IdMunicipio { get; set; }
        public string Direccion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual Estado? IdEstadoNavigation { get; set; } = null!;
        public virtual Municipio? IdMunicipioNavigation { get; set; } = null!;
        public virtual ICollection<Medico>? Medicos { get; set; }
    }
}

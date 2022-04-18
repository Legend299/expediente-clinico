using System;
using System.Collections.Generic;

namespace webservice1.Data
{
    public partial class Estado
    {
        public Estado()
        {
            Expedientes = new HashSet<Expediente>();
            Hospitales = new HashSet<Hospitale>();
            MunicipiosEstados = new HashSet<MunicipiosEstado>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Expediente> Expedientes { get; set; }
        public virtual ICollection<Hospitale> Hospitales { get; set; }
        public virtual ICollection<MunicipiosEstado> MunicipiosEstados { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace webservice1.Data
{
    public partial class MunicipiosEstado
    {
        public int Id { get; set; }
        public int IdEstado { get; set; }
        public int IdMunicipio { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual Municipio IdMunicipioNavigation { get; set; } = null!;
    }
}

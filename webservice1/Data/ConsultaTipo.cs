using System;
using System.Collections.Generic;

namespace webservice1.Data
{
    public partial class ConsultaTipo
    {
        public ConsultaTipo()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int IdTipoConsulta { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            Medicos = new HashSet<Medico>();
        }

        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}

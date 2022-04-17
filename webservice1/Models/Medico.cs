using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Medico
    {
        public int IdMedico { get; set; }
        public int IdUsuario { get; set; }
        public string Cedula { get; set; } = null!;
        public int IdEspecialidad { get; set; }
        public int IdHospital { get; set; }

        public virtual Especialidade IdEspecialidadNavigation { get; set; } = null!;
        public virtual Hospitale IdHospitalNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}

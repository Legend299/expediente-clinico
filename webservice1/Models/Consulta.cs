using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Consulta
    {
        public int IdConsulta { get; set; }
        public DateOnly Fecha { get; set; }
        public string Medico { get; set; }
        public int IdTipoConsulta { get; set; }
        public string Diagnostico { get; set; } = null!;
        public int IdExpediente { get; set; }

        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
        public virtual ConsultaTipo IdTipoConsultaNavigation { get; set; } = null!;
    }
}

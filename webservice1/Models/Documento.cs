using System;
using System.Collections.Generic;

namespace webservice1.Models
{
    public partial class Documento
    {
        public int IdDocumento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public string Ruta { get; set; } = null!;
        public string Medico { get; set; }
        public int Peso { get; set; }
        public int IdExpediente { get; set; }

        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace webservice1.Data
{
    public partial class ExpedientesPermiso
    {
        public int IdPermiso { get; set; }
        public int IdUsuario { get; set; }
        public int IdExpediente { get; set; }
        public bool Permiso { get; set; }

        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}

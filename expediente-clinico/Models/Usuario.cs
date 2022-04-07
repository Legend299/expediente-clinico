using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Models
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string Curp { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public byte IdRol { get; set; }
        public long IdExpediente { get; set; }
    }
}

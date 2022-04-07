using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Models
{
    public class Expediente
    {
        public long IdExpediente { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Direccion { get; set; }
        public bool Sexo { get; set; }
        public string Curp { get; set; }
    }
}

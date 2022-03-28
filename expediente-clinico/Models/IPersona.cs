using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Models
{
    interface IPersona
    {
        public long Id { get; set; }
        public String Nombre { get; set; }
        public String Imagen { get; set; }
        public String Apellido { get; set; }
        public String Telefono { get; set; }
        public String Correo { get; set; }
        public bool Sexo { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Models
{
    public class Medico : IPersona
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Sexo { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public Especialidad Especialidad { get; set; }
        public Hospital Hospital { get; set; }
    }
}

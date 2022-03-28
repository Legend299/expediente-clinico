using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("[ERROR] --> Elemento no encontrado en la base de datos") { }
    }
}

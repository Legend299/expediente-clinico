using expediente_clinico.Models;
using expediente_clinico.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Repository
{
    public class EspecialidadRepository : IEspecialidadesRepository
    {
        public void actualizarEspecialidad(Especialidad especialidad, long id)
        {
            throw new NotImplementedException();
        }

        public void agregarEspecialidad(Especialidad medico)
        {
            throw new NotImplementedException();
        }

        public void borrarEspecialidad(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Especialidad> listarEspecialidades()
        {
            throw new NotImplementedException();
        }

        public Especialidad listarEspecialidadPorId(long id)
        {
            throw new NotImplementedException();
        }
    }
}

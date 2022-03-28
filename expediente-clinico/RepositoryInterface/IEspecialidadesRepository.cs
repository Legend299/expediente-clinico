using expediente_clinico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.RepositoryInterface
{
    interface IEspecialidadesRepository
    {
        public IEnumerable<Especialidad> listarEspecialidades();
        public Especialidad listarEspecialidadPorId(long id);
        public void actualizarEspecialidad(Especialidad especialidad, long id);
        public void borrarEspecialidad(long id);
        public void agregarEspecialidad(Especialidad medico);
    }
}

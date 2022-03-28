using expediente_clinico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.RepositoryInterface
{
    interface IPacienteRepository
    {
        public IEnumerable<Paciente> listarPacientes();
        public Paciente listarPacientePorId(long id);
        public void actualizarMedico(Paciente paciente, long id);
        public void borrarMedico(long id);
        public void agregarMedico(Paciente paciente);
    }
}

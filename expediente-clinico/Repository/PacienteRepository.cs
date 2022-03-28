using expediente_clinico.Models;
using expediente_clinico.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        public void actualizarMedico(Paciente paciente, long id)
        {
            throw new NotImplementedException();
        }

        public void agregarMedico(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public void borrarMedico(long id)
        {
            throw new NotImplementedException();
        }

        public Paciente listarPacientePorId(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Paciente> listarPacientes()
        {
            throw new NotImplementedException();
        }
    }
}

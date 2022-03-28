using expediente_clinico.Models;
using expediente_clinico.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Repository
{
    public class MedicoRepository : IMedicoRepository
    {
        public void actualizarMedico(Medico medico, long id)
        {
            throw new NotImplementedException();
        }

        public void agregarMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public void borrarMedico(long id)
        {
            throw new NotImplementedException();
        }

        public Medico listarMedicoPorId(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medico> listarMedicos()
        {
            throw new NotImplementedException();
        }
    }
}

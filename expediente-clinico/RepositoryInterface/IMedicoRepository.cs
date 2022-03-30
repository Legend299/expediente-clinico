using expediente_clinico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.RepositoryInterface
{
    interface IMedicoRepository
    {
        public Task<List<Medico>> listarMedicos();
        public Task<Medico> listarMedicoPorId(long id);
        public void actualizarMedico(Medico medico, long id);
        public void borrarMedico(long id);
        public void agregarMedico(Medico medico);
    }
}

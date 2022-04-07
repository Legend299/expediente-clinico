using expediente_clinico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.RepositoryInterface
{
    interface IExpedienteRepository
    {
        public Task<List<Expediente>> listarExpedientes();
        public Task<Expediente> listarExpedientePorId(long id);
        public Task<Expediente> obtenerExpedientePorCurp(String curp);
        public void actualizarExpediente(Expediente expediente, string curp);
        public void borrarExpediente(long id);
        public void agregarExpediente(Expediente expediente);
    }
}

using expediente_clinico.Models;
using expediente_clinico.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Repository
{
    public class HospitalRepository : IHospitalRepository
    {
        public void actualizarHospital(Hospital hospital, long id)
        {
            throw new NotImplementedException();
        }

        public void agregarHospital(Hospital hospital)
        {
            throw new NotImplementedException();
        }

        public void borrarHospital(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hospital> listarHospitales()
        {
            throw new NotImplementedException();
        }

        public Hospital listarHospitalPorId(long id)
        {
            throw new NotImplementedException();
        }
    }
}

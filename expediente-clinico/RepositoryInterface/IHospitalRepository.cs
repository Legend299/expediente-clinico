using expediente_clinico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.RepositoryInterface
{
    interface IHospitalRepository
    {
        public IEnumerable<Hospital> listarHospitales();
        public Hospital listarHospitalPorId(long id);
        public void actualizarHospital(Hospital hospital, long id);
        public void borrarHospital(long id);
        public void agregarHospital(Hospital hospital);
    }
}

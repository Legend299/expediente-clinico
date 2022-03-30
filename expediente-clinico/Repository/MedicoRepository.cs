using expediente_clinico.Models;
using expediente_clinico.RepositoryInterface;
using expediente_clinico.RepositorySqlInterface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Repository
{
    public class MedicoRepository : IMedicoRepository
    {
        ConexionMySQL mySql = new ConexionMySQL();
        public void actualizarMedico(Medico medico, long id)
        {
            throw new NotImplementedException();
        }

        public async void agregarMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public async void borrarMedico(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Medico> listarMedicoPorId(long id)
        {
            string query = "";
            return null;
        }

        public async Task<List<Medico>> listarMedicos()
        {
            //string query = "SELECT * FROM medicos";
            string query = "SELECT " +
                "medicos.IdMedico," +
                "medicos.Nombre," +
                "medicos.Apellido," +
                "medicos.Imagen," +
                "medicos.Telefono," +
                "medicos.Correo," +
                "medicos.Sexo," +
                "medicos.FechaDeNacimiento," +
                "especialidades.IdEspecialidad," +
                "especialidades.Nombre," +
                "especialidades.Descripcion," +
                "hospitales.IdHospital," +
                "hospitales.Nombre," +
                "hospitales.Direccion," +
                "hospitales.Telefono," +
                "hospitales.Correo" +
                " FROM " +
                "medicos medicos" +
                " INNER JOIN " +
                "especialidades especialidades" +
                " ON medicos.IdEspecialidad = especialidades.IdEspecialidad " +
                "INNER JOIN " +
                "hospitales hospitales" +
                " ON medicos.IdHospital = hospitales.IdHospital";
            MySqlConnection con = await mySql.GetConexionAsync();
            return await ListarMedicosSql(con, query);
        }

        public async Task<List<Medico>> ListarMedicosSql(MySqlConnection mySqlConnection, string query)
        {
            List<Medico> medicos = new List<Medico>();

            if (mySqlConnection == null)
                mySqlConnection = await mySql.GetConexionAsync();

            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            using (DbDataReader sqlReader = await cmd.ExecuteReaderAsync())
                while (sqlReader.Read())
                    medicos.Add(LeerMedico(sqlReader));

            return medicos;
        }

        public Medico LeerMedico(DbDataReader sqlReader)
        {
            Medico medico = new Medico();
            Especialidad especialidad = new Especialidad();
            Hospital hospital = new Hospital();
  
            medico.Id = Convert.ToInt64(sqlReader[0]);
            medico.Nombre = sqlReader[1].ToString();
            medico.Apellido = sqlReader[2].ToString();
            medico.Imagen = sqlReader[3].ToString();
            medico.Telefono = sqlReader[4].ToString();
            medico.Correo = sqlReader[5].ToString();
            medico.Sexo = Convert.ToBoolean(sqlReader[6]);
            medico.FechaDeNacimiento = Convert.ToDateTime(sqlReader[7]);

            especialidad.Id = Convert.ToInt64(sqlReader[8]);
            especialidad.Nombre = sqlReader[9].ToString();
            especialidad.Descripcion = sqlReader[10].ToString();

            medico.Especialidad = especialidad;

            hospital.Id = Convert.ToInt64(sqlReader[11]);
            hospital.Nombre = sqlReader[12].ToString();
            hospital.Direccion = sqlReader[13].ToString();
            hospital.Telefono = sqlReader[14].ToString();

            medico.Hospital = hospital;

            return medico;
        }
    }
}

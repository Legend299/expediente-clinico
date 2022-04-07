using expediente_clinico.Models;
using expediente_clinico.RepositoryInterface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        ConexionMySQL mySql = new ConexionMySQL();
        public void actualizarUsuario(Usuario usuario, long id)
        {
            throw new NotImplementedException();
        }

        public async void agregarUsuario(Usuario usuario)
        {
            MySqlConnection con = await mySql.GetConexionAsync();
            Console.WriteLine("USUARIO CORREO: "+usuario.Correo);
            AgregarUsuarioSql(con, usuario);
        }

        public void borrarUsuario(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> listarUsuarioPorId(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Usuario>> listarUsuarios()
        {
            //string query = "SELECT * FROM medicos";
            string query = "SELECT * FROM usuarios";
            MySqlConnection con = await mySql.GetConexionAsync();
            return await ListarUsuariosSql(con, query);
        }

        public void AgregarUsuarioSql(MySqlConnection mySqlConnection, Usuario usuario) 
        {
            MySqlCommand comm = mySqlConnection.CreateCommand();
            comm.CommandText = "INSERT INTO usuarios(Curp,Correo,Contrasena,IdRol,IdExpediente) VALUES(?Curp, ?Correo, ?Contrasena, ?IdRol, ?IdExpediente)";
            comm.Parameters.Add("Curp", MySqlDbType.VarChar).Value = usuario.Curp;
            comm.Parameters.Add("?Correo", MySqlDbType.VarChar).Value = usuario.Correo;
            comm.Parameters.Add("?Contrasena", MySqlDbType.VarChar).Value = usuario.Contrasena;
            comm.Parameters.Add("?IdRol", MySqlDbType.Byte).Value = usuario.IdRol;
            comm.Parameters.Add("?IdExpediente", MySqlDbType.Int64).Value = usuario.IdExpediente;
            comm.ExecuteNonQuery();
        }

        public async Task<List<Usuario>> ListarUsuariosSql(MySqlConnection mySqlConnection, string query)
        {
            List<Usuario> usuarios = new List<Usuario>();

            if (mySqlConnection == null)
                mySqlConnection = await mySql.GetConexionAsync();

            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            using (DbDataReader sqlReader = await cmd.ExecuteReaderAsync())
                while (sqlReader.Read())
                    usuarios.Add(LeerUsuario(sqlReader));

            return usuarios;
        }

        public Usuario LeerUsuario(DbDataReader sqlReader)
        {
            Usuario usuario = new Usuario();


            usuario.IdUsuario = Convert.ToInt64(sqlReader[0]);
            usuario.Curp = sqlReader[1].ToString();
            usuario.Correo = sqlReader[2].ToString();
            usuario.Contrasena = sqlReader[3].ToString();
            usuario.IdRol = Convert.ToByte(sqlReader[4]);
            var obj = sqlReader[5];
            if (obj != DBNull.Value)
                usuario.IdExpediente = Convert.ToInt64(sqlReader[5]);
                //usuario.IdExpediente = Convert.ToInt64(sqlReader[5]);
            /*medico.Id = Convert.ToInt64(sqlReader[0]);
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

            medico.Hospital = hospital;*/

            return usuario;
        }
    }
}

﻿using expediente_clinico.Models;
using expediente_clinico.RepositoryInterface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.Repository
{
    public class ExpedienteRepository : IExpedienteRepository
    {
        ConexionMySQL mySql = new ConexionMySQL();
        public async void actualizarExpediente(Expediente expediente, string curp)
        {
            MySqlConnection con = await mySql.GetConexionAsync();
            ActualizarExpedienteSql(con, expediente, curp);
        }

        public async void agregarExpediente(Expediente expediente)
        {
            
            MySqlConnection con = await mySql.GetConexionAsync();
            InsertarExpedienteSql(con,expediente);

        }

        public void borrarExpediente(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Expediente> listarExpedientePorId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Expediente>> listarExpedientes()
        {
            throw new NotImplementedException();
        }

        public async Task<Expediente> obtenerExpedientePorCurp(string curp)
        {
            string query = "SELECT * FROM expedientes WHERE Curp='" + curp + "'";
            MySqlConnection con = await mySql.GetConexionAsync();
            return await ObtenerExpedientePorCurpSql(con, query);
        }

        public void InsertarExpedienteSql(MySqlConnection mySqlConnection, Expediente expediente)
        {
            MySqlCommand comm = mySqlConnection.CreateCommand();
            comm.CommandText = "INSERT INTO expedientes(Imagen,Nombre,Apellido,Telefono,FechaDeNacimiento,Direccion,Sexo,Curp) VALUES(?Imagen, ?Nombre, ?Apellido, ?Telefono, ?FechaDeNacimiento, ?Direccion, ?Sexo, ?Curp)";
            comm.Parameters.Add("?Imagen", MySqlDbType.VarChar).Value = expediente.Imagen;
            comm.Parameters.Add("?Nombre", MySqlDbType.VarChar).Value = expediente.Nombre;
            comm.Parameters.Add("?Apellido", MySqlDbType.VarChar).Value = expediente.Apellido;
            comm.Parameters.Add("?Telefono", MySqlDbType.VarChar).Value = expediente.Telefono;
            comm.Parameters.Add("?FechaDeNacimiento", MySqlDbType.Date).Value = expediente.FechaDeNacimiento;
            comm.Parameters.Add("?Direccion", MySqlDbType.VarChar).Value = expediente.Direccion;
            comm.Parameters.Add("?Sexo", MySqlDbType.Byte).Value = expediente.Sexo;
            comm.Parameters.Add("?Curp", MySqlDbType.VarChar).Value = expediente.Curp;
            comm.ExecuteNonQuery();
        }

        public void ActualizarExpedienteSql(MySqlConnection mySqlConnection, Expediente expediente, string curp) 
        {
            MySqlCommand comm = mySqlConnection.CreateCommand();
            comm.CommandText = "UPDATE expedientes SET Imagen=?Imagen, Nombre=?Nombre , Apellido=?Apellido, Telefono=?Telefono, FechaDeNacimiento=?FechaDeNacimiento, Direccion=?Direccion, Sexo=?Sexo, Curp=?Curp WHERE Curp = '"+curp+"'";
            comm.Parameters.Add("?Imagen", MySqlDbType.VarChar).Value = expediente.Imagen;
            comm.Parameters.Add("?Nombre", MySqlDbType.VarChar).Value = expediente.Nombre;
            comm.Parameters.Add("?Apellido", MySqlDbType.VarChar).Value = expediente.Apellido;
            comm.Parameters.Add("?Telefono", MySqlDbType.VarChar).Value = expediente.Telefono;
            comm.Parameters.Add("?FechaDeNacimiento", MySqlDbType.Date).Value = expediente.FechaDeNacimiento;
            comm.Parameters.Add("?Direccion", MySqlDbType.VarChar).Value = expediente.Direccion;
            comm.Parameters.Add("?Sexo", MySqlDbType.Byte).Value = expediente.Sexo;
            comm.Parameters.Add("?Curp", MySqlDbType.VarChar).Value = expediente.Curp;
            comm.ExecuteNonQuery();
        }

        public async Task<Expediente> ObtenerExpedientePorCurpSql(MySqlConnection mySqlConnection, string query)
        {
            Expediente expediente = new Expediente();

            if (mySqlConnection == null)
                mySqlConnection = await mySql.GetConexionAsync();

            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            using (DbDataReader sqlReader = await cmd.ExecuteReaderAsync())
                while (sqlReader.Read())
                    expediente = LeerExpediente(sqlReader);

            return expediente;
        }

        public Expediente LeerExpediente(DbDataReader sqlReader)
        {
            Expediente expediente = new Expediente();

            expediente.IdExpediente = Convert.ToInt64(sqlReader[0]);
            expediente.Imagen = sqlReader[1].ToString();
            expediente.Nombre = sqlReader[2].ToString();
            expediente.Apellido = sqlReader[3].ToString();
            expediente.Telefono = sqlReader[4].ToString();
            expediente.FechaDeNacimiento = Convert.ToDateTime(sqlReader[5]);
            expediente.Direccion = sqlReader[6].ToString();
            expediente.Sexo = Convert.ToBoolean(sqlReader[7]);
            expediente.Curp = sqlReader[8].ToString();

            return expediente;
        }
    }
}

using expediente_clinico.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico
{
    public class ConexionMySQL
    {
        private const string HOST = "SERVER=127.0.0.1;PORT=3306;DATABASE=expediente_medico;UID=root;PASSWORDS=;";
        public async Task<MySqlConnection> GetConexionAsync() {
            MySqlConnection con = new MySqlConnection(HOST);
            if (con.State == ConnectionState.Closed)
                await con.OpenAsync();
            return con;
        }
    }
}

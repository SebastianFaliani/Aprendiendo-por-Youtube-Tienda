using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Tienda.COMMON.Intefaces;

namespace Tienda.DAL.MSSqlServer
{
    public class DBMSSqlServer : IDB
    {
        private SqlConnection conexion;

        public DBMSSqlServer()
        {
            string server = "tcp:DESKTOP-AJIHV72,1433";
            string database = "tienda";
            string uid = "sa";
            string password = "S0l_tech";

            conexion = new SqlConnection(string.Format("Server={0};DataBase={1}; User Id={2}; Password={3};", server, database, uid, password));
            Conectar();
        }

        private bool Conectar()
        {
            try
            {
                conexion.Open();
                Error = "";
                return true;
            }
            catch (SqlException ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public string Error { get; private set; }

        public bool Comando(string command)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(command, conexion);
                cmd.ExecuteNonQuery();
                Error = "";
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public object Consulta(string consulta)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                SqlDataReader datosLeidos = cmd.ExecuteReader();
                Error = "";
                return datosLeidos;
                
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        ~DBMSSqlServer()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }

        }
    }
}

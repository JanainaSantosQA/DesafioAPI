using System;
using Dapper;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AutomacaoApiMantis.Helpers
{
    public class DBHelpers
    {
        public static MySqlConnection GetDBConnection()
        {
            string connectionString = "Server=" + JsonBuilder.ReturnParameterAppSettings("DB_URL") + ";" +
                                      "Port=" + JsonBuilder.ReturnParameterAppSettings("DB_PORT") + ";" +
                                      "Database=" + JsonBuilder.ReturnParameterAppSettings("DB_NAME") + ";" +
                                      "UID=" + JsonBuilder.ReturnParameterAppSettings("DB_USER") + ";" +
                                      "Password=" + JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD") + ";" +
                                      "Allow User Variables=True" + ";" +
                                      "SslMode=" + JsonBuilder.ReturnParameterAppSettings("DB_SSLMODE");

            MySqlConnection connection = new MySqlConnection(connectionString);

            return connection;
        }
        public static void ExecuteQuery(string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        public static List<T> ObtemLista<T>(string query)
        {
            IEnumerable<T> retorno = null;

            using (IDbConnection db = GetDBConnection())
            {    
                retorno = db.Query<T>(query);
            }

            return retorno.ToList();
        }
        public static T ObtemRegistroUnico<T>(string query)
        {
            T retorno;

            using (IDbConnection db = GetDBConnection())
            {
                retorno = db.Query<T>(query).FirstOrDefault();
            }

            return retorno;
        }
        public static List<string> ObtemDados(String query, String[,] arrayDeParam = null)
        {
            DataSet ds = new DataSet();
            List<string> lista = new List<string>();

            using (MySqlCommand cmd = new MySqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
                cmd.Connection.Open();

                if (arrayDeParam != null)
                {
                    int i = 0;
                    while (i < arrayDeParam.GetLength(0))
                    {
                        cmd.Parameters.AddWithValue(arrayDeParam[i, 0], arrayDeParam[i, 1]);
                        i++;
                    }
                }

                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);
                cmd.Connection.Close();
            }

            if (ds.Tables[0].Columns.Count == 0)
            {
                return null;
            }

            try
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    lista.Add(ds.Tables[0].Rows[0][i].ToString());
                }
            }
            catch (Exception)
            {

                return null;
            }

            return lista;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace LyricalMiracle.Data
{
    public class LoginData : DB
    {
        OleDbConnection connection = new OleDbConnection(connectionString);

        public string GetHashedPassword(string username)
        {
            DataTable tableUser = new DataTable();
            string passwordDB = string.Empty;

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(getHashedPassword, connection))
            {
                connection.Open();

                adapter.SelectCommand.Parameters.AddWithValue("@Username", username);
                adapter.Fill(tableUser);

                if (tableUser.Rows.Count > 0)
                {
                    passwordDB = tableUser.Rows[0]["Password"].ToString();
                }

                connection.Close();
            }

            return passwordDB;
        }

        #region SqlCommands

        public static string getHashedPassword = "SELECT Password FROM [User] WHERE Username = @Username";

        #endregion
    }
}

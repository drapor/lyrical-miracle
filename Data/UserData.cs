using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using LyricalMiracle.Entities;
using System.Data;

namespace LyricalMiracle.Data
{
    public class UserData : DB
    {
        OleDbConnection connection = new OleDbConnection(connectionString);

        public User GetUser(string username)
        {
            DataTable tableUser = new DataTable();
            User user = new User();

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(getUser, connection))
            {
                connection.Open();

                adapter.SelectCommand.Parameters.AddWithValue("@Username", username);
                adapter.Fill(tableUser);

                if (tableUser.Rows.Count > 0)
                {
                    user.ID = Guid.Parse(tableUser.Rows[0]["ID"].ToString());
                    user.Username = tableUser.Rows[0]["Username"].ToString();
                    user.Email = tableUser.Rows[0]["Email"].ToString();

                    int fame = 0;
                    user.Fame = fame;
                    if(Int32.TryParse(tableUser.Rows[0]["Fame"].ToString(), out fame))
                    {
                        user.Fame = fame;
                    }
                }

                connection.Close();
            }

            return user;
        }

        public void SetConnected(Guid IDUser, bool State)
        {
            using (OleDbCommand command = new OleDbCommand("UPDATE [User] SET Connected = '" + Convert.ToInt32(State) + "' WHERE ID = @IDUser", connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@IDUser", IDUser);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        #region SqlCommands

        public static string getUser = "SELECT * FROM [User] WHERE Username = @Username";

        #endregion
    }
}

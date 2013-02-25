using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace LyricalMiracle.Data
{
    public class StatsData : DB
    {
        OleDbConnection connection = new OleDbConnection(connectionString);

        public void WriteStats(Guid IDUser, Enums.StatsType StatsType)
        {
            using (OleDbCommand command = new OleDbCommand(incrementLogin, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@ID", Guid.NewGuid());
                command.Parameters.AddWithValue("@IDUser", IDUser);
                command.Parameters.AddWithValue("@Type", (int)StatsType);
                command.Parameters.AddWithValue("@Date", DateTime.Now);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        #region SqlCommands

        public static string incrementLogin = "INSERT INTO Stats (ID, IDUser, Type, [Date]) VALUES (@ID, @IDUser, @Type, @Date)";

        #endregion
    }
}

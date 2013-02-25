using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using LyricalMiracle.Entities;

namespace LyricalMiracle.Data
{
    public class MeaningsData : DB
    {
        OleDbConnection connection = new OleDbConnection(connectionString);

        public List<Meanings> GetMeanings(Guid IDUser)
        {
            DataTable tableMeanings = new DataTable();
            List<Meanings> meanings = new List<Meanings>();

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(getMeanings, connection))
            {
                connection.Open();

                adapter.SelectCommand.Parameters.AddWithValue("@IDUser", IDUser);
                adapter.Fill(tableMeanings);

                for (int i = 0; i < tableMeanings.Rows.Count; i++)
                {
                    Meanings meaning = new Meanings();

                    meaning.ID = Guid.Parse(tableMeanings.Rows[i]["Meaning.ID"].ToString());
                    meaning.IDUser = IDUser;
                    meaning.StartChar = Int32.Parse(tableMeanings.Rows[i]["StartChar"].ToString());
                    meaning.EndChar = Int32.Parse(tableMeanings.Rows[i]["EndChar"].ToString());
                    meaning.Meaning = tableMeanings.Rows[i]["Meaning"].ToString();
                    meaning.NbVoteUp = Int32.Parse(tableMeanings.Rows[i]["NbVoteUp"].ToString());
                    meaning.NbVoteDown = Int32.Parse(tableMeanings.Rows[i]["NbVoteDown"].ToString());
                    meaning.Lyrics = tableMeanings.Rows[i]["Lyrics"].ToString();
                    meaning.IDLyrics = Guid.Parse(tableMeanings.Rows[i]["Lyrics.ID"].ToString());
                    meaning.IDSong = Guid.Parse(tableMeanings.Rows[i]["Song.ID"].ToString());
                    meaning.SongName = tableMeanings.Rows[i]["Song.Title"].ToString();
                    meaning.IDAlbum = Guid.Parse(tableMeanings.Rows[i]["Album.ID"].ToString());
                    meaning.AlbumName = tableMeanings.Rows[i]["Album.Title"].ToString();
                    meaning.IDArtist = Guid.Parse(tableMeanings.Rows[i]["Artist.ID"].ToString());
                    meaning.ArtistName = tableMeanings.Rows[i]["ArtistName"].ToString();

                    meanings.Add(meaning);
                }

                connection.Close();
            }

            return meanings;
        }

        #region SqlCommands

        public static string getMeanings = "SELECT Meaning.ID, Meaning.IDUser, Meaning.StartChar, Meaning.EndChar, Meaning.Meaning, Meaning.NbVoteUp, Meaning.NbVoteDown, Lyrics.Lyrics, Lyrics.ID, Song.ID, Song.Title, Album.ID, Album.Title, Artist.ID, Artist.ArtistName " +
                                         "FROM [User] INNER JOIN (((Artist INNER JOIN Album ON Artist.ID = Album.IDArtist) INNER JOIN Song ON Album.ID = Song.IDAlbum) INNER JOIN (Lyrics INNER JOIN Meaning ON Lyrics.ID = Meaning.IDLyrics) ON Song.ID = Lyrics.IDSong) ON (User.ID = Meaning.IDUser) AND (User.ID = Lyrics.IDUser) " +
                                         "WHERE (((Meaning.IDUser)=@IDUser));";

        #endregion
    }
}

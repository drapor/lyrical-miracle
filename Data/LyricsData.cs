using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using LyricalMiracle.Entities;

namespace LyricalMiracle.Data
{
    public class LyricsData : DB
    {
        OleDbConnection connection = new OleDbConnection(connectionString);

        public List<Lyrics> GetLyrics(Guid IDUser)
        {
            DataTable tableLyrics = new DataTable();
            List<Lyrics> lyrics = new List<Lyrics>();

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(getLyrics, connection))
            {
                connection.Open();

                adapter.SelectCommand.Parameters.AddWithValue("@IDUser", IDUser);
                adapter.Fill(tableLyrics);

                for (int i = 0; i < tableLyrics.Rows.Count; i++)
	            {
                    Lyrics lyric = new Lyrics();

                    lyric.ID = Guid.Parse(tableLyrics.Rows[i]["Lyrics.ID"].ToString());
                    lyric.IDUser = IDUser;
                    lyric.IDSong = Guid.Parse(tableLyrics.Rows[i]["Song.ID"].ToString());
                    lyric.IDAlbum = Guid.Parse(tableLyrics.Rows[i]["Album.ID"].ToString());
                    lyric.IDArtist = Guid.Parse(tableLyrics.Rows[i]["Artist.ID"].ToString());
                    lyric.SongName = tableLyrics.Rows[i]["Song.Title"].ToString();
                    lyric.AlbumName = tableLyrics.Rows[i]["Album.Title"].ToString();
                    lyric.ArtistName = tableLyrics.Rows[i]["ArtistName"].ToString();
                    lyric.NbVoteUp = Int32.Parse(tableLyrics.Rows[i]["NbVoteUp"].ToString());
                    lyric.NbVoteDown = Int32.Parse(tableLyrics.Rows[i]["NbVoteDown"].ToString());
                    lyric.Rating = Math.Round(lyric.NbVoteUp / (lyric.NbVoteUp + (lyric.NbVoteDown)) * 100).ToString() + "%";
                    lyric.Lyric = tableLyrics.Rows[i]["Lyrics"].ToString();

                    lyrics.Add(lyric);
	            }

                connection.Close();
            }

            return lyrics;
        }

        #region SqlCommands

        public static string getLyrics = "SELECT Lyrics.ID, Lyrics.IDUser, Lyrics.IDSong, Lyrics.NbVoteUp, Lyrics.NbVoteDown, Lyrics.Lyrics, Song.ID, Song.IDAlbum, Song.Title, Album.ID, Album.IDArtist, Album.Title, Artist.ID, Artist.ArtistName " +
                                        "FROM ((Artist INNER JOIN Album ON Artist.ID = Album.IDArtist) INNER JOIN Song ON Album.ID = Song.IDAlbum) INNER JOIN Lyrics ON Song.ID = Lyrics.IDSong " +
                                        "WHERE (((Lyrics.IDUser)= @IDUser));";

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LyricalMiracle.Entities;

namespace LyricalMiracle.Data
{
    public class TrackData : DB
    {
        public string GetLyricsPerTrack(Track currentTrack)
        {
            string CurrentLyrics = string.Empty;
            DataTable DTTrack = new DataTable();

            //using (SqlConnection connection = new SqlConnection(DB.connectionString))
            //{
            //    using (SqlDataAdapter getCommand = new SqlDataAdapter(selectBidon, connection))
            //    {
            //        connection.Open();
            //        //getCommand.SelectCommand.Parameters.AddWithValue("@Title", currentTrack.Name);
            //        //getCommand.SelectCommand.Parameters.AddWithValue("@Name", currentTrack.Artist);

            //        getCommand.Fill(DTTrack);

            //        for (int i = 0; i < DTTrack.Rows.Count; i++)
            //        {
            //            CurrentLyrics = DTTrack.Rows[i]["Lyrics"].ToString();
            //        }

            //        connection.Close();
            //    }
            //}
            return CurrentLyrics;
        }


        public void InsertEveryting(Artist artist)
        {
             using (SqlConnection connection = new SqlConnection(DB.connectionString))
            {
                using (SqlCommand insertCommandArtist = new SqlCommand(insertArtist, connection))
                {
                    connection.Open();

                    insertCommandArtist.Parameters.AddWithValue("@IDArtist", artist.ID);
                    insertCommandArtist.Parameters.AddWithValue("@Name", artist.Name);

                    insertCommandArtist.ExecuteNonQuery();
                }

                foreach (Album album in artist.Albums)
                {
                    using (SqlCommand insertCommandAlbum = new SqlCommand(insertAlbum, connection))
                    {
                        insertCommandAlbum.Parameters.AddWithValue("@IDAlbum", album.ID);
                        insertCommandAlbum.Parameters.AddWithValue("@IDArtist", artist.ID);
                        insertCommandAlbum.Parameters.AddWithValue("@Name", album.Name);

                        insertCommandAlbum.ExecuteNonQuery();
                    }

                    foreach (Song song in album.Songs)
                    {
                        using (SqlCommand insertCommandSong = new SqlCommand(insertSong, connection))
                        {
                            insertCommandSong.Parameters.AddWithValue("@IDSong", song.ID);
                            insertCommandSong.Parameters.AddWithValue("@IDAlbum", album.ID);
                            insertCommandSong.Parameters.AddWithValue("@Title", song.Title);
                            insertCommandSong.Parameters.AddWithValue("@Duration", song.Duration);
                            insertCommandSong.Parameters.AddWithValue("@Lyrics", song.Lyrics);
                            insertCommandSong.Parameters.AddWithValue("@Link", song.Link);

                            insertCommandSong.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        #region Static Commands

        public static string selectLyrics = "SELECT Song.Lyrics " +
                                           "FROM Song INNER JOIN " +
                                           "Album ON Song.IDAlbum = Album.IDAlbum INNER JOIN " +
                                           "Artist ON Album.IDArtist = Artist.IDArtist " +
                                           "WHERE (Artist.Name = @Name AND Song.Title = @Title)";
        public static string selectBidon = "SELECT Lyrics FROM Song WHERE IDSong = '5ACDD3F9-0A0F-40E1-9AD7-0000B77D757F'";

        public const String insertArtist = "INSERT INTO Artist VALUES (@IDArtist, @Name, @Link)";
        public const String insertAlbum = "INSERT INTO Album VALUES (@IDAlbum, @IDArtist, @Name)";
        public const String insertSong = "INSERT INTO Song VALUES (@IDSong, @IDAlbum, @Title, @Duration, @Lyrics, @Link)";

        #endregion
    }
}

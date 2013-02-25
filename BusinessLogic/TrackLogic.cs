using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricalMiracle.Data;
using System.Data.SqlClient;
using LyricalMiracle.Entities;

namespace LyricalMiracle.BusinessLogic
{
    public class TrackLogic
    {
        private TrackData TDInstance = new TrackData();
        private FormatSong FSInstance = new FormatSong();

        public string GetLyricsPerTrack(Track currentTrack)
        {
            string currentLyrics = string.Empty;
            try
            {
                currentLyrics = TDInstance.GetLyricsPerTrack(currentTrack);

                if (currentLyrics == string.Empty)
                {
                    FSInstance.FormatTrack(currentTrack);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return currentLyrics;
        }

        public void InsertEverything(Artist artist)
        {
            try
            {
                TDInstance.InsertEveryting(artist);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
    }
}

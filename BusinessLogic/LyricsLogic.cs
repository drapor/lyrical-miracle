using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricalMiracle.Data;
using LyricalMiracle.Entities;

namespace LyricalMiracle.BusinessLogic
{
    public class LyricsLogic
    {
        Exceptions exceptionInstance = new Exceptions();
        LyricsData lyricsDataInstance = new LyricsData();

        public List<Lyrics> GetLyrics(Guid IDUser)
        {
            List<Lyrics> lyrics = new List<Lyrics>();

            try
            {
                lyrics = lyricsDataInstance.GetLyrics(IDUser);

            }
            catch (Exception ex)
            {
                exceptionInstance.WriteException(Guid.NewGuid(), ex, "LyricsLogic", "GetLyrics", DateTime.Now);
            }

            return lyrics;
        }
    }
}

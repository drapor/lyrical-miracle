using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricalMiracle.Entities;

namespace LyricalMiracle
{
    public class FormatSong
    {
        public Track FormatTrack(Track track)
        {
            if (track.Name.Contains('-'))
            {
                track.Name = track.Name.Replace('-', ' ');
            }
            if (track.Name.Contains("feat"))
            {
                track.Name = track.Name.Substring(0, track.Name.IndexOf("feat"));
            }

            return track;
        }
    }
}

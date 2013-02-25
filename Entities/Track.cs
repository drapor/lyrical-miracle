using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace LyricalMiracle.Entities
{
    public class Track
    {
        public Track()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; }
        public IITArtworkCollection Artwork { get; set; }
        public int BitRate { get; set; }
        public int BPM { get; set; }
        public string Lyrics { get; set; }
    }
}

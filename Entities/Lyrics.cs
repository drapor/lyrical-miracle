using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricalMiracle.Entities
{
    public class Lyrics
    {
        public Lyrics()
        {
            ID = Guid.NewGuid();
            Meanings = new List<Meanings>();
        }

        public Guid ID { get; set; }
        public Guid IDUser { get; set; }
        public Guid IDSong { get; set; }
        public Guid IDAlbum { get; set; }
        public Guid IDArtist { get; set; }
        public string SongName { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public double NbVoteUp { get; set; }
        public double NbVoteDown { get; set; }
        public string Lyric { get; set; }
        public string Rating { get; set; }

        public List<Meanings> Meanings { get; set; }
    }
}

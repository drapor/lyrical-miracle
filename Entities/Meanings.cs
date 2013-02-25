using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricalMiracle.Entities
{
    public class Meanings
    {
        public Meanings()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public Guid IDUser { get; set; }
        public Guid IDLyrics { get; set; }
        public Guid IDSong { get; set; }
        public Guid IDAlbum { get; set; }
        public Guid IDArtist { get; set; }
        public string SongName { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int NbVoteUp { get; set; }
        public int NbVoteDown { get; set; }
        public int StartChar { get; set; }
        public int EndChar { get; set; }
        public string Meaning { get; set; }
        public string Lyrics { get; set; }

        public double Rating
        {
            get { return NbVoteUp / (NbVoteUp + NbVoteDown); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricalMiracle
{
    public class Song
    {
        public Song()
        {
            ID = new Guid();
        }
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Lyrics { get; set; }
        public string Link { get; set; }
    }
}

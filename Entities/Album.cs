using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricalMiracle.Entities
{
    public class Album
    {
        public Album()
        {
            ID = new Guid();
            Songs = new List<Song>();
        }
        public Guid ID { get; set; }
        public Guid IDArtist { get; set; }
        public List<Song> Songs { get; set; }
        public string Name { get; set; }
    }
}

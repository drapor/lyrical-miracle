using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricalMiracle.Entities
{
    public class Artist
    {
        public Artist()
        {
            ID = Guid.NewGuid();
            Albums = new List<Album>();
        }
        public Guid ID{ get; set; }
        public List<Album> Albums { get; set; }
        public string Name { get; set; }
    }
}

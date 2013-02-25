using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricalMiracle.Entities
{
    public class User
    {
        public User()
        {
            ID = Guid.NewGuid();
            Lyrics = new List<Lyrics>();
            Meanings = new List<Meanings>();
        }
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Fame { get; set; }
        public List<Lyrics> Lyrics { get; set; }
        public List<Meanings> Meanings { get; set; }
    }
}

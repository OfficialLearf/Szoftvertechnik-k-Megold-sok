using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp
{
    class Song
    {
        public readonly string Artist;
        public readonly string Title;

        public Song(string artist, string title)
        {
            Artist = artist;
            Title = title;
        }

        public override string ToString()
        {
            return $"{Artist}: {Title}\n";
        }
    }
}

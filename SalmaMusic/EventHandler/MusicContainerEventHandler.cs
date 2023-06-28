using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.EventHandlers
{
    public class MusicContainerEventHandler : EventArgs
    {
        public List<Music> musics = new List<Music>();
    }
}

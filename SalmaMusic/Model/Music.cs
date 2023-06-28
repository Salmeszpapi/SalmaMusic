using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.Model
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }


        public Music(string name,string path)
        {
            this.Name = name;
            this.Path = path;
        }

    }
}

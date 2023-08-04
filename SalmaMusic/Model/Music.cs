using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SalmaMusic.Model
{
    [Table("Music")]
    public class Music : EventArgs
    {
        [Key]
        public int Id { get; set; }     
        public string Name { get; set; }
        public string Path { get; set; }

        public Music(string name,string path)
        {
            this.Name = name;
            this.Path = path;
        }
        public Music()
        {
            
        }

    }
}

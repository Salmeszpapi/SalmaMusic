using SalmaMusic.Model;
using SalmaMusic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalmaMusic.ViewModel
{
    public class MainSiteViewModel
    {
        public UserControl MusicContainer { get; set; }
        private string _musicTimer = "1:15/3:15";
        public string MusicTimer
        {
            get { return _musicTimer; }
            set {
                _musicTimer = value;
                
            }
        }

        public MainSiteViewModel()
        {
            MusicContainer = new MusicContentModel().GetView();
        }

    }
}

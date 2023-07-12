using SalmaMusic.Model;
using SalmaMusic.MVVM.ViewModels;
using SalmaMusic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalmaMusic.MVVM.Models
{
    public class MusicContentModel : BaseModel
    {
        private MusicContentView musicContentView;
        private MusicContentViewModel musicContentViewModel;

        public MusicContentModel()
        {
            musicContentViewModel = new MusicContentViewModel();
            musicContentView = new MusicContentView() { DataContext = musicContentViewModel };
            WorkFlowManager.SaveUsercontrol(this);
        }
        public UserControl GetView()
        {
            return musicContentView;
        }
    }
}

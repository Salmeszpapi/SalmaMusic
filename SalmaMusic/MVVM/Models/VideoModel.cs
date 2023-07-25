using SalmaMusic.Model;
using SalmaMusic.MVVM.ViewModels;
using SalmaMusic.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalmaMusic.MVVM.Models
{
    public class VideoModel : BaseModel
    {
        private VideoView VideoView;
        private VideoViewModel VideoViewModel;
        public VideoModel()
        {
            VideoViewModel = new VideoViewModel();
            VideoView = new VideoView() { DataContext = VideoViewModel};
            WorkFlowManager.SaveUsercontrol(this);
        }

        public override UserControl GetView()
        {
            return VideoView;
        }
    }
}

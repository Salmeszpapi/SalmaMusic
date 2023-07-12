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
    public class ExploreModel : BaseModel
    {
        private ExploreView exploreView;
        private ExploreViewModel exploreViewModel;

        public ExploreModel()
        {
            exploreViewModel = new ExploreViewModel();
            exploreView = new ExploreView() { DataContext = exploreViewModel };
            WorkFlowManager.SaveUsercontrol(this);
        }
        public UserControl GetView()
        {
            return exploreView;
        }
    }
}

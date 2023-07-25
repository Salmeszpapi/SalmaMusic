using SalmaMusic.Model;
using SalmaMusic.MVVM.ViewModels;
using SalmaMusic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SalmaMusic.MVVM.Models
{
    internal class MainSiteModel : BaseModel
    {
        private MainSiteViewModel MainSiteViewModel;
        private MainSiteView mainSiteView;
        public MainSiteModel()
        {
            MainSiteViewModel = new MainSiteViewModel();
            mainSiteView = new MainSiteView() { DataContext = MainSiteViewModel };
            WorkFlowManager.SaveUsercontrol(this);
        }
        public Window GetWindow()
        {
            return mainSiteView;
        }
        
        public override UserControl GetView()
        {
            throw new NotImplementedException();
        }
    }
}

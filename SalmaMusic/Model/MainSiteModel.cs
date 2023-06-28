using SalmaMusic.View;
using SalmaMusic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SalmaMusic.Model
{
    internal class MainSiteModel
    {
        private MainSiteViewModel MainSiteViewModel;
        private MainSiteView mainSiteView;
        public MainSiteModel()
        {
            MainSiteViewModel = new MainSiteViewModel();
            mainSiteView = new MainSiteView() { DataContext = MainSiteViewModel};
        }
        public Window GetWindow() 
        {
            return mainSiteView;
        }
    }
}

using SalmaMusic.View;
using SalmaMusic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.Model
{
    public class ExploreModel : BaseModel
    {
        private ExploreView exploreView;
        private ExploreViewModel exploreViewModel;

        public ExploreModel()
        {
            exploreViewModel = new ExploreViewModel();
            exploreView = new ExploreView() { DataContext = exploreViewModel};
            WorkFlowManager.SaveUsercontrol(this);
        }
    }
}

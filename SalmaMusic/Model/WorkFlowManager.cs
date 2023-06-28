using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalmaMusic.Model
{

    public static class WorkFlowManager
    {
        public static Dictionary<string, BaseModel> myUserControls = new Dictionary<string, BaseModel>();

        public static bool SaveUsercontrol(BaseModel baseModel)
        {
            if (!myUserControls.ContainsKey(baseModel.GetType().Name))
            {
                myUserControls.Add(baseModel.GetType().Name, baseModel);
                return true;
            }
            return false;
        }
        public static BaseModel GetUsercontrol(string baseModel)
        {
            if (myUserControls.ContainsKey(baseModel))
            {
                return myUserControls[baseModel];
            }
            return null;
        }
    }
}

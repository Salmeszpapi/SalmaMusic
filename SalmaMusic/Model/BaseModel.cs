using SalmaMusic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalmaMusic.Model
{
    public abstract class BaseModel
    {
        public abstract UserControl GetView();
    }
}

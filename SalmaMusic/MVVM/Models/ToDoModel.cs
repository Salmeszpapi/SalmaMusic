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
    public class ToDoModel : BaseModel
    {
        private ToDoViewModel viewModel;
        private ToDoView ToDoView;

        public ToDoModel()
        {
            viewModel = new ToDoViewModel();
            ToDoView = new ToDoView() { DataContext = viewModel};
        }

        public override UserControl GetView()
        {
            return ToDoView;
        }
    }
}

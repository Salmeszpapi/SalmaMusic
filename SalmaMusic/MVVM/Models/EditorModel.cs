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
    class EditorModel : BaseModel
    {
        private EditorViewModel editorViewModel;
        private EditorView editorView;

        public EditorModel()
        {
            editorViewModel = new EditorViewModel();
            editorView = new EditorView() { DataContext = editorViewModel};
        }

        public override UserControl GetView()
        {
            return editorView;
        }
    }
}

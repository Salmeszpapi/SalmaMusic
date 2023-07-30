using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;

namespace SalmaMusic.MVVM.ViewModels
{
    public class VideoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private DispatcherTimer _timer;
        public List<Brush> ColorPicker { get; set; }
        public ICommand DeleteCanvasContent { get; set; }

        private int _EllipseJumpingFrequency
        {
            get { return _EllipseJumpingFrequency; }
            set 
            {
                _EllipseJumpingFrequency = value;
                
            }
        }

        public VideoViewModel() 
        {
            DeleteCanvasContent = new RelayCommand(CanvasContentDeleteCommand);
            ColorPicker = new List<Brush> { Brushes.AliceBlue, Brushes.Black, Brushes.Blue, Brushes.BurlyWood };
        }

        private void CanvasContentDeleteCommand()
        {
        }

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}

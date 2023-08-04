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
        public List<ColorItem> Colors { get; set; }
        public ICommand DeleteCanvasContent { get; set; }

        private int _EllipseJumpingFrequency
        {
            get { return _EllipseJumpingFrequency; }
            set 
            {
                _EllipseJumpingFrequency = value;
                
            }
        }
        private ColorItem selectedColorItem;

        public ColorItem SelectedColorItem
        {
            get { return selectedColorItem; }
            set
            {
                selectedColorItem = value;
                // You can perform additional logic here if needed
            }
        }

        public VideoViewModel() 
        {
            DeleteCanvasContent = new RelayCommand(CanvasContentDeleteCommand);
            Colors = new List<ColorItem>() { 
                new ColorItem() { ColorBrush = Brushes.AliceBlue, DisplayName = "AliceBlue" },
                new ColorItem() { ColorBrush = Brushes.White, DisplayName = "White" },
                new ColorItem() { ColorBrush = Brushes.Black, DisplayName = "Black" },
                new ColorItem() { ColorBrush = Brushes.Green, DisplayName = "Green" },
                new ColorItem() { ColorBrush = Brushes.Blue, DisplayName = "Blue" },
                new ColorItem() { ColorBrush = Brushes.Yellow, DisplayName = "Yellow" }
            };
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

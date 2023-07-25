using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SalmaMusic.MVVM.ViewModels
{
    public class VideoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private DispatcherTimer _timer;

        private int _EllipseJumpingFrequency
        {
            get { return _EllipseJumpingFrequency; }
            set 
            {
                _EllipseJumpingFrequency = value;
                
            }
        }

        public int EllipseJumpingFrequency { get; set; } = 0;

        public VideoViewModel() 
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(20);
            _timer.Tick += UpdateCanvas;
            //MainSiteViewModel.timer.Tick += (sender, e) => { 
                
            //};
            _timer.Start();
            _timer.Tick += (sender, e) =>
            {
                Console.WriteLine("asd");
            };
        }
        private void UpdateCanvas(object sender, EventArgs e)
        {
            EllipseJumpingFrequency++;
            if(EllipseJumpingFrequency > 300) 
            {
                EllipseJumpingFrequency = 0;
            }
            OnPropertyChanged(nameof(EllipseJumpingFrequency));
        }

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}

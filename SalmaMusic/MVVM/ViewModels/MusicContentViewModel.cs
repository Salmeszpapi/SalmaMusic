using DynamicData;
using ReactiveUI;
using SalmaMusic.EventHandlers;
using SalmaMusic.Model;
using SalmaMusic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SalmaMusic.MVVM.ViewModels
{
    public class MusicContentViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Music> _Music = new ObservableCollection<Music>();
        public static event EventHandler<Music> MusicChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public MusicContentViewModel()
        {
            MainSiteViewModel.menuButtonEventClicked += MusicLoader;
            MainSiteViewModel.MusicSkipped += MusicSkipped;
        }

        public static void MusicSkipped(object? sender, Music e)
        {
            //SelectedItem = e;
            //Items.SelectedItems.Add(SelectedItem);
        }

        private Music _SelectedItem { get; set; }
        public Music SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                ChangedSelectedItem(value);
            }
        }
        public ObservableCollection<Music> Items
        {
            get { return _Music; }
            set { _Music = value; }
        }


        private async void MusicLoader(object sender, MusicContainerEventHandler e)
        {
            Items.Add(e.musics.ToList());
        }
        private void ChangedSelectedItem(Music music)
        {
            MusicChanged.Invoke(this, music);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

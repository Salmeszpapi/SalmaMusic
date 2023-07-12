using DynamicData;
using ReactiveUI;
using SalmaMusic.EventHandlers;
using SalmaMusic.Model;
using SalmaMusic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.ViewModel
{
    public class MusicContentViewModel
    {
        private ObservableCollection<Music> _Music = new ObservableCollection<Music>();
        public static event EventHandler<Music> MusicChanged;
        private Music _SelectedItem { get;set; }
        public Music SelectedItem {
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


        public MusicContentViewModel()
        {
            MainSiteViewModel.menuButtonEventClicked += MusicLoader;
        }
        private async void MusicLoader(object sender, MusicContainerEventHandler e)
        { 
            Items.Add(e.musics.ToList());
        }
        private void ChangedSelectedItem(Music music)
        {
            MusicChanged.Invoke(this, music);
        }
    }
}

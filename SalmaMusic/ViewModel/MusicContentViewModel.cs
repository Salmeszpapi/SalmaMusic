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
        private List<string> _localList = new List<string>();
        private ObservableCollection<string> _Music = new ObservableCollection<string>();
        public ObservableCollection<string> Items
        {
            get { return _Music; }
            set { _Music = value; }
        }
        public string? TestText => "My first test text in Music content user content, then why the heell the list binding is not working huh ....";


        public MusicContentViewModel()
        {
            MainSiteView.menuButtonEventClicked += MusicLoader;
        }
        private async void MusicLoader(object sender, MusicContainerEventHandler e)
        { 
            Items.Add(new ObservableCollection<string>(e.musics.Select(x => x.Name).ToList()));
        }
    }
}

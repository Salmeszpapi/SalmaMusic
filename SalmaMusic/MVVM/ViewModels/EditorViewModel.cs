using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalmaMusic.MVVM.ViewModels
{
    class EditorViewModel
    {

        private ObservableCollection<Music> _Myproperty { get; set; }
        private Music _SelectedItem { get; set; }
        public ObservableCollection<Music> MyProperty
        {
            get { return _Myproperty; }
            set
            {
                _Myproperty = value;

            }
        }
        public Music SelectedItem
        {
            get
            {
                if (MyProperty.Any())
                {
                    _SelectedItem = MyProperty[0];
                }
                return _SelectedItem;

            }
            set
            {
                _SelectedItem = value;
            }
        }
        public EditorViewModel()
        {
            MyProperty = new ObservableCollection<Music>(MainSiteViewModel.songList);

        }


    }
}

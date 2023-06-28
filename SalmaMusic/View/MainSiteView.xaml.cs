using DynamicData;
using Microsoft.Win32;
using SalmaMusic.EventHandlers;
using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalmaMusic.View
{
    /// <summary>
    /// Interaction logic for MainSiteView.xaml
    /// </summary>
    public partial class MainSiteView : Window
    {
        public static event EventHandler<MusicContainerEventHandler> menuButtonEventClicked;
        private bool musicCurrentStatus = false;
        private MediaPlayer m_mediaPlayer;
        public MainSiteView()
        {
            InitializeComponent();
        }
        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            List<Music> songList = new List<Music>();
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true };
            openFileDialog.ShowDialog(this);
            MusicContentModel musicContentModel = new MusicContentModel();
            WorkFlowManager.SaveUsercontrol(musicContentModel);
            MusicContainer = musicContentModel.GetView();


            foreach (var file in openFileDialog.FileNames)
            {
                Music newMusic = new Music("Test", file);
                songList.Add(newMusic);
            }
            menuButtonEventClicked?.Invoke(this, new MusicContainerEventHandler() { musics = songList });

        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var test = WorkFlowManager.GetUsercontrol("MusicContentModel");
           
        }

        private void PlayMusic(object sender, MouseButtonEventArgs e)
        {
            
            var sri = "D:/musics/punanny.wav";
            var asd = new Uri("D:/musics/punanny.wav");
            //if (musicPlayer.PersistId == 1)
            //{
            //    musicPlayer.Name = "playing";
            //}
            var a = (ImageSource)Resources["Project"];
            if (!musicCurrentStatus)
            {
                musicPlayer.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\pause.png");
                musicCurrentStatus = true;
                Play(sri);
            }
            else
            {
                musicPlayer.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\play.png");
                musicCurrentStatus = false;
                Stop();

            }

            var test = m_mediaPlayer.Position;


        }


        private void Play(string filename)
        {
            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(new Uri(filename));
            m_mediaPlayer.Play();

        }
        private void Stop()
        {
            m_mediaPlayer.Pause();
        }

        // `volume` is assumed to be between 0 and 100.
        public void SetVolume(int volume)
        {
            // MediaPlayer volume is a float value between 0 and 1.
            m_mediaPlayer.Volume = volume / 100.0f;
        }
    }
}

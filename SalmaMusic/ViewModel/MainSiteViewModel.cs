using SalmaMusic.Model;
using SalmaMusic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SalmaMusic.Assets;
using System.IO;
using System.Windows;
using System.Data.Common;
using SalmaMusic.EventHandlers;
using NAudio.Wave;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SalmaMusic.ViewModel
{
    public class MainSiteViewModel : INotifyPropertyChanged
    {
        public static event EventHandler<MusicContainerEventHandler> menuButtonEventClicked;
        public UserControl MusicContainer { get; set; }
        private string _musicTimer = "1:15/3:18";

        public event PropertyChangedEventHandler? PropertyChanged;
        private string _MusicButton { get; set; }
        public ICommand MyStartMusicButtonIsPressed { get; set; }
        public ICommand MyBackMusicButtonIsPressed { get; set; }
        public ICommand MySkippMusicButtonIsPressed { get; set; }
        public ICommand MyFolderButtonIsPressed { get; set; }
        public ICommand FavouritesContentPage { get; set; }
        public ICommand ExploreContentPage { get; set; }

        private string CurrentMusicPath { get; set; }
        private MediaPlayer m_mediaPlayer { get; set; }
        private MusicStatusEnum musicStatusEnum { get; set; } = MusicStatusEnum.Unknown;
        //private Timer MusicTimer { get; set; }
        private DispatcherTimer timer;

        public MainSiteViewModel()
        {
            MusicContainer = new MusicContentModel().GetView();
            MusicContentViewModel.MusicChanged += changeMusicEventHandling;
            MyStartMusicButtonIsPressed = new RelayCommand(StartMusicButtonClicked);
            MyBackMusicButtonIsPressed = new RelayCommand(BackMusicButtonClicked);
            MySkippMusicButtonIsPressed = new RelayCommand(SkippMusicButtonClicked);
            MyFolderButtonIsPressed = new RelayCommand(FolderClicked);
            FavouritesContentPage = new RelayCommand(SwitchToMusicContentPage);
            ExploreContentPage = new RelayCommand(SwitchToExploreContent);
            menuButtonEventClicked += MainSiteViewModel_menuButtonEventClicked;
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\play.png"));
            BackgroundBrush = imageBrush;
            m_mediaPlayer =  new MediaPlayer();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);  // Update the displayed time every 500 milliseconds
            timer.Tick += Timer_Tick;
        }

        private void SwitchToExploreContent()
        {
            throw new NotImplementedException();
        }

        private void SwitchToMusicContentPage()
        {
            MusicContentModel musicContent = (MusicContentModel)WorkFlowManager.GetUsercontrol(nameof(MusicContentModel));
            if (musicContent is not null)
            {
                MusicContainer = musicContent.GetView();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MusicTimer = m_mediaPlayer.Position.TotalSeconds + m_mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
        }

        private void MainSiteViewModel_menuButtonEventClicked(object? sender, MusicContainerEventHandler e)
        {

        }

        private Brush _backgroundBrush;
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set
            {
                _backgroundBrush = value;
                OnPropertyChanged(nameof(BackgroundBrush));
            }
        }


        public string MusicName
        {
            get { return _MusicButton; }
            set { 
                _MusicButton = value;
                OnPropertyChanged(nameof(MusicName));
            }
        }

        public string MusicTimer
        {
            get { return _musicTimer; }
            set {
                _musicTimer = value;
                OnPropertyChanged(nameof(MusicTimer));
            }
        }

        private void changeMusicEventHandling(object? sender, Music e)
        {
            MusicName = e.Name;
            CurrentMusicPath = e.Path;
            m_mediaPlayer.Open(new Uri(CurrentMusicPath));
            PlayMusic();
            if (m_mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                MusicTimer = m_mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StartMusicButtonClicked()
        {
            if(CurrentMusicPath is null)
            {
                return;
            }
            switch (musicStatusEnum)
            {       //stop the music
                case MusicStatusEnum.Playing:
                    PauseMusic();
                    break;
                //continue playing the music
                case MusicStatusEnum.Paused:
                    PlayMusic();
                    break;
                //play the music from begining 
                case MusicStatusEnum.Unknown:
                    PlayMusic();
                    break;
                case MusicStatusEnum.Stopped:
                    PlayMusic();
                    break;
            }
        }

        private void BackMusicButtonClicked()
        {

        }

        private void SkippMusicButtonClicked()
        {

        }
        private void PlayMusic()
        {
            m_mediaPlayer.Play();
            musicStatusEnum = MusicStatusEnum.Playing;
            BackgroundBrush = new ImageBrush() { ImageSource = new BitmapImage(new Uri("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\pause.png")) };
            MusicTimer = m_mediaPlayer.NaturalDuration.ToString();
        }

        private void PauseMusic()
        {
            m_mediaPlayer.Pause();
            musicStatusEnum = MusicStatusEnum.Stopped;
            BackgroundBrush = new ImageBrush() { ImageSource = new BitmapImage(new Uri("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\play.png")) };
        }

        private void StopMusic()
        {
            m_mediaPlayer.Stop();
            musicStatusEnum = MusicStatusEnum.Stopped;
            BackgroundBrush = new ImageBrush() { ImageSource = new BitmapImage(new Uri("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\play.png")) };
        }

        private void FolderClicked()
        {
            List<Music> songList = new List<Music>();
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true, Filter = "Audio WAV/Mp3 Files (*.wav;*.mp3)|*.wav;*.mp3" };
            openFileDialog.ShowDialog();
            MusicContentModel musicContentModel = new MusicContentModel();
            WorkFlowManager.SaveUsercontrol(musicContentModel);
            MusicContainer = musicContentModel.GetView();



            foreach (var file in openFileDialog.FileNames)
            {
                var finalFile = FormatChecker(file);
                Music newMusic = new Music(finalFile.Split("\\").Last(), finalFile);
                songList.Add(newMusic);
            }
            var disctinctMusics = new List<Music>();
            var namesSet = new HashSet<string>();

            foreach (var musics in songList)
            {
                if (namesSet.Add(musics.Name))
                {
                    disctinctMusics.Add(musics);
                }
            }

            songList = disctinctMusics;
            //var clearedList = songList.Select(x=>x.Name.Distinct());
            menuButtonEventClicked?.Invoke(this, new MusicContainerEventHandler() { musics = songList.ToList() });
        }

        private string FormatChecker(string music)
        {
            if (!music.Contains(".mp3"))
            {
                return music;
            }
            //transform the path tring
            var newPathString = music.Split('.')[0] + ".wav";
            using (Mp3FileReader mp3Reader = new Mp3FileReader(music))
            {
                // Create a WaveFileWriter to write the WAV file
                using (WaveFileWriter wavWriter = new WaveFileWriter(newPathString, mp3Reader.WaveFormat))
                {
                    // Convert and write each frame from MP3 to WAV
                    byte[] buffer = new byte[mp3Reader.WaveFormat.AverageBytesPerSecond];
                    int bytesRead;
                    do
                    {
                        bytesRead = mp3Reader.Read(buffer, 0, buffer.Length);
                        wavWriter.Write(buffer, 0, bytesRead);
                    } while (bytesRead > 0);

                    // Finalize the WAV file
                    wavWriter.Flush();
                }
            }
            return newPathString;
        }
    }
}

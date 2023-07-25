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
using SalmaMusic.MVVM.Models;
using System.Threading;
using SalmaMusic.DbHelper;
using System.Windows.Data;

namespace SalmaMusic.MVVM.ViewModels
{
    public class MainSiteViewModel : INotifyPropertyChanged
    {
        public static event EventHandler<MusicContainerEventHandler> menuButtonEventClicked;
        public static event EventHandler<Music> MusicSkipped;
        private UserControl _MusicContainer;

        private string _musicTimer = "1:15/3:18";

        public event PropertyChangedEventHandler? PropertyChanged;
        private string _MusicButton { get; set; }
        public ICommand MyStartMusicButtonIsPressed { get; set; }
        public ICommand MyBackMusicButtonIsPressed { get; set; }
        public ICommand MySkippMusicButtonIsPressed { get; set; }
        public ICommand MyFolderButtonIsPressed { get; set; }
        public ICommand FavouritesContentPage { get; set; }
        public ICommand ExploreContentPage { get; set; }
        public ICommand EditorContentPage { get; set; }
        public ICommand VideoContentPage { get; set; }
        public ICommand GearButtonIsPressed { get; set; }


        private string CurrentMusicPath { get; set; }
        private MediaPlayer m_mediaPlayer { get; set; }
        private MusicStatusEnum musicStatusEnum { get; set; } = MusicStatusEnum.Unknown;
        //private Timer MusicTimer { get; set; }
        public static DispatcherTimer timer;
        public static List<Music> songList = new List<Music>();
        private Music actualSongPlaying;
        private MyDataProvider m_dataProvider;

        public MainSiteViewModel()
        {
            MusicContainer = new MusicContentModel().GetView();
            MusicContentViewModel.MusicChanged += changeMusicEventHandling;
            MyStartMusicButtonIsPressed = new RelayCommand(StartMusicButtonClicked);
            MyBackMusicButtonIsPressed = new RelayCommand(BackMusicButtonClicked);
            MySkippMusicButtonIsPressed = new RelayCommand(SkippMusicButtonClicked);
            MyFolderButtonIsPressed = new RelayCommand(FolderClicked);
            GearButtonIsPressed = new RelayCommand(GearClicked);
            FavouritesContentPage = new RelayCommand(SwitchToMusicContentPage);
            ExploreContentPage = new RelayCommand(SwitchToExploreContent);
            EditorContentPage = new RelayCommand(SwitchToEditorPage);
            VideoContentPage = new RelayCommand(VideoToEditorPage);
            menuButtonEventClicked += MainSiteViewModel_menuButtonEventClicked;
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\play.png"));
            BackgroundBrush = imageBrush;
            m_mediaPlayer = new MediaPlayer();
            m_dataProvider = new MyDataProvider();
            LoadMusicsFromDb();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);  // Update the displayed time every 500 milliseconds
            timer.Tick += (sender, e) =>
            {
                try
                {
                    MusicTimer = m_mediaPlayer.Position.ToString(@"mm\:ss") + " : " + m_mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                    CurrentPosition = m_mediaPlayer.Position.TotalSeconds;
                }
                catch (Exception ex)
                {

                }
            };
            m_mediaPlayer.MediaEnded += (sender, e) =>
            {
                SkippMusicButtonClicked();
            };
        }

        private void GearClicked()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();
        }

        private void LoadMusicsFromDb()
        {
            var asd = m_dataProvider.Music.ToList();
            m_dataProvider.Music.ToList().ForEach(music => { songList.Add(music); });
            menuButtonEventClicked?.Invoke(this, new MusicContainerEventHandler() { musics = songList.ToList() });
        }

        private void VideoToEditorPage()
        {
            VideoModel videoModel = (VideoModel)WorkFlowManager.GetUsercontrol(nameof(VideoModel));
            if (videoModel is null)
            {
                videoModel = new VideoModel();
            }
            MusicContainer = videoModel.GetView();
        }

        private void SwitchToEditorPage()
        {
            EditorModel exploreContent = (EditorModel)WorkFlowManager.GetUsercontrol(nameof(EditorModel));
            if (exploreContent is null)
            {
                exploreContent = new EditorModel();
            }
            MusicContainer = exploreContent.GetView();
        }

        private void SwitchToExploreContent()
        {
            ExploreModel exploreContent = (ExploreModel)WorkFlowManager.GetUsercontrol(nameof(ExploreModel));
            if (exploreContent is null)
            {
                exploreContent = new ExploreModel();
            }
            MusicContainer = exploreContent.GetView();
        }

        private void SwitchToMusicContentPage()
        {
            MusicContentModel musicContent = (MusicContentModel)WorkFlowManager.GetUsercontrol(nameof(MusicContentModel));
            if (musicContent is null)
            {
                musicContent = new MusicContentModel();
            }
            MusicContainer = musicContent.GetView();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {

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

        public UserControl MusicContainer
        {
            get
            {
                return _MusicContainer;
            }
            set
            {
                _MusicContainer = value;
                OnPropertyChanged(nameof(MusicContainer));
            }
        }


        public string MusicName
        {
            get { return _MusicButton; }
            set
            {
                _MusicButton = value;
                OnPropertyChanged(nameof(MusicName));
            }
        }

        public string MusicTimer
        {
            get { return _musicTimer; }
            set
            {
                _musicTimer = value;
                OnPropertyChanged(nameof(MusicTimer));
            }
        }
        private double currentPosition = 0;
        public double CurrentPosition
        {
            get => currentPosition;
            set
            {
                currentPosition = value;
                m_mediaPlayer.Position = TimeSpan.FromSeconds(currentPosition);
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }
        private double totalDuration { get; set; }
        public double TotalDuration
        {
            get => totalDuration;
            set
            {
                totalDuration = value;
                OnPropertyChanged(nameof(TotalDuration));
            }
        }
        private double _musicVolume { get; set; } = 0.5;
        public double MusicVolume
        {
            get => _musicVolume;
            set
            {
                OnPropertyChanged(nameof(_musicVolume));
                m_mediaPlayer.Volume = value;
                _musicVolume = value;
            }
        }

        private void changeMusicEventHandling(object? sender, Music e)
        {
            actualSongPlaying = e;
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
            if (CurrentMusicPath is null)
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
            if (musicStatusEnum is not MusicStatusEnum.Unknown)
            {
                if (actualSongPlaying is not null)
                {
                    var nextSoundIndex = songList.FindIndex(x => x == actualSongPlaying);
                    if (nextSoundIndex != songList.Count-1)
                    {
                        nextSoundIndex++;
                    }
                    else
                    {
                        nextSoundIndex= 0;
                    }
                    if (nextSoundIndex < songList.Count)
                    {
                        MusicName = songList[nextSoundIndex].Name;
                        CurrentMusicPath = songList[nextSoundIndex].Path;
                        m_mediaPlayer.Open(new Uri(CurrentMusicPath));
                        actualSongPlaying = songList[nextSoundIndex];
                        PlayMusic();
                        if (m_mediaPlayer.NaturalDuration.HasTimeSpan)
                        {
                            MusicTimer = m_mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                            MusicSkipped.Invoke(this, songList[nextSoundIndex]);
                        }
                    }
                }
            }
        }
        private void PlayMusic()
        {
            timer.Start();
            m_mediaPlayer.Play();
            musicStatusEnum = MusicStatusEnum.Playing;
            BackgroundBrush = new ImageBrush() { ImageSource = new BitmapImage(new Uri("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\pause.png")) };
            Thread.Sleep(400);
            if (m_mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                double musicLength = m_mediaPlayer.NaturalDuration.TimeSpan.Minutes * 60 + m_mediaPlayer.NaturalDuration.TimeSpan.Seconds;
                TotalDuration = musicLength;
            }
        }

        private void PauseMusic()
        {
            timer.Stop();
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

            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true, Filter = "Audio Wav (*.wav)|*.wav" };
            openFileDialog.ShowDialog();
            MusicContentModel musicContentModel = new MusicContentModel();
            WorkFlowManager.SaveUsercontrol(musicContentModel);
            
            m_dataProvider.Database.EnsureCreated();
            
            
            
            //MusicContainer = musicContentModel.GetView();


            foreach (var file in openFileDialog.FileNames)
            {
                var finalFile = FormatChecker(file);
                Music newMusic = new Music(finalFile.Split("\\").Last(), finalFile);
                m_dataProvider.Music.Add(newMusic);
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
            m_dataProvider.SaveChanges();
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

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
using System;
using System.Reflection;
using System.Resources;
using NAudio.Wave;
using SalmaMusic.MVVM.ViewModels;
using SalmaMusic.MVVM.Models;

namespace SalmaMusic.View
{
    /// <summary>
    /// Interaction logic for MainSiteView.xaml
    /// </summary>
    public partial class MainSiteView : Window
    {
        private bool musicCurrentStatus = false;
        private MediaPlayer m_mediaPlayer;
        public BitmapImage BackButton;
        public string MusicName { get; set; }
        public string MusicTimer { get; set; }
        private string CurrentMusicPath { get; set; }

        public MainSiteView()
        {
            InitializeComponent();
        }


        private void changeMusicEventHandling(object? sender, Music e)
        {
            CurrentMusicPath = "D:\\musics\\" + e.Name;
            MusicName = e.Name;
            Stop();
            Play(CurrentMusicPath);
        }


        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            List<Music> songList = new List<Music>();
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true, Filter = "Audio WAV/Mp3 Files (*.wav;*.mp3)|*.wav;*.mp3" };
            openFileDialog.ShowDialog(this);
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
            //menuButtonEventClicked?.Invoke(this, new MusicContainerEventHandler() { musics = songList.ToList() });
        }

        private void PlayMusic(object sender, MouseButtonEventArgs e)
        {

            if(CurrentMusicPath == null)
            {
                //error no music loaded 
                Show();
                return;
            }

            if (!musicCurrentStatus)
            {
                musicPlayer.Background = (Brush)new ImageSourceConverter().ConvertFromString("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\pause.png");
                musicCurrentStatus = true;
                Play(CurrentMusicPath);
            }
            else
            {
                musicPlayer.Background = (Brush)new ImageSourceConverter().ConvertFromString("C:\\Users\\salma\\source\\repos\\SalmaMusic\\SalmaMusic\\Assets\\Images\\play.png");
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
            try
            {

            }catch (Exception ex)
            {
                m_mediaPlayer.Pause();
            }

        }

        // `volume` is assumed to be between 0 and 100.
        public void SetVolume(int volume)
        {
            // MediaPlayer volume is a float value between 0 and 1.
            m_mediaPlayer.Volume = volume / 100.0f;
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

using Microsoft.Win32;
using NAudio.Wave;
using SalmaMusic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.Model
{
    class MediaPlayerHelper : IMediaPlayer
    {
        public MediaPlayerHelper() 
        {

        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void LoadMusic(Music music)
        {
            //if music format is MP3 then its converted to Wav
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true, Filter = "Audio Wav (*.wav)|*.wav" };
            openFileDialog.ShowDialog();
            MusicMaker(FormatConverter(music));
        }

        private void MusicMaker(string music)
        {

        }

        private string FormatConverter(Music music)
        {
            string musicPath = music.Path;
            if (!musicPath.Contains(".mp3"))
            {
                return musicPath;
            }
            //transform the path tring
            var newPathString = musicPath.Split('.')[0] + ".wav";
            using (Mp3FileReader mp3Reader = new Mp3FileReader(musicPath))
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

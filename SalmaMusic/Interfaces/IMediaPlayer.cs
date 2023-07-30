using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.Interfaces
{
    interface IMediaPlayer
    {
        void Play();
        void Pause();
        void Stop();
        void LoadMusic(Music music);

    }
}

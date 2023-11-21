using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GameEngine.GameServices
{
    public static class MusicPlayer
    {
        private static MediaPlayer _mediaPlayer = new MediaPlayer();//הנגן 
        public static bool isOn { get; set; } = false;
        public static void Play(string FileName)
        {
            isOn = true;
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"/Assets/music/{FileName}"));
            _mediaPlayer.IsLoopingEnabled = true;
            _mediaPlayer.Play();
        }
        public static void Stop()
        {
            isOn = false;
            _mediaPlayer.Pause();
        }
        public static void ChangeVolume(double volume)
        {
            _mediaPlayer.Volume = volume / 100;
        }
    }
}

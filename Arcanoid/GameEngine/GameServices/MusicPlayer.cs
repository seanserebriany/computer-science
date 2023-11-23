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
        private static double _volume = 0.5;
        public static bool isOn { get; set; } = false;
        public static double Volume
        {
            set
            {
                _volume = Volume / 100;
                _mediaPlayer.Volume = Volume / 100;
            }
            get
            {
                return _volume * 100;
            }
        }
        public static void Play(string FileName)
        {
            if(!isOn)
            {
            isOn = true;
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{FileName}"));
            _mediaPlayer.IsLoopingEnabled = true;
            _mediaPlayer.Play();
            }
        }
        public static void Stop()
        {
            isOn = false;
            _mediaPlayer.Pause();
        }
    }
}

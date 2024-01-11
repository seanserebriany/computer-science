using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GameEngine.GameServices
{
    public class BackgroundSound
    {
        private static MediaPlayer _mediaPlayer = new MediaPlayer();
        public static bool isOn = false;
        public static void SetVolume(double value)
        {
            _mediaPlayer.Volume = value / 100;
        }
        public static double GetVolume()
        {
            return _mediaPlayer.Volume * 100;
        }
        public static void Play(string filename)
        {
            isOn = true;
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{filename}"));
            _mediaPlayer.IsLoopingEnabled = true;
            _mediaPlayer.Play();
        }
        public static void Stop()
        {
            isOn = false;
            _mediaPlayer.Pause();
        }

    }
}

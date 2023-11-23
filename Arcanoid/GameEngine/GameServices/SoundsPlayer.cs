using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GameEngine.GameServices
{
    public static class SoundsPlayer
    {
        public static MediaPlayer _mediaPlayer = new MediaPlayer();
        public static bool isOn { get; set;} = false;
        public static void Play(string fileName)
        {
            if (isOn)
            {
                _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Sounds/{fileName}"));
                _mediaPlayer.Play();
            }
        }
        public static void Stop()
        {
            _mediaPlayer.Pause();
        }
    }
}

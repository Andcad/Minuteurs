using Foundation;
using Xamarin.Forms;
using Minuteurs.iOS;
using AVFoundation;

[assembly: Dependency(typeof(AudioRender))]
namespace Minuteurs.iOS
{
    class AudioRender : IAudioService
    {
        // Tutoriel youtube = https://youtu.be/U6QMRgmjmac
        public void PlayAudioFile(string filename)
        {
            var player = new AVAudioPlayer(new NSUrl(filename), "MP3", out NSError error);
            player.FinishedPlaying += delegate
            {
                player = null;
            };
            player.Play();
        }
    }
}
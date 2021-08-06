using Android.Media;
using Minuteurs.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioRender))]
namespace Minuteurs.Droid
{
    public class AudioRender : IAudioService
    {
        // Tutoriel youtube = https://youtu.be/U6QMRgmjmac
        public void PlayAudioFile(string filename)
        {
            var player = new MediaPlayer();
            var file = global::Android.App.Application.Context.Assets.OpenFd(filename);
            player.SetDataSource(file.FileDescriptor, file.StartOffset, file.Length);
            player.Prepared += (s, e) => { player.Start(); };
            player.Prepare();
        }
    }
}
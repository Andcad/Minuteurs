using Minuteurs.UWP;
using System;
using Windows.ApplicationModel;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioRender))]
namespace Minuteurs.UWP
{
    class AudioRender : IAudioService
    {
        // Tutoriel youtube = https://youtu.be/U6QMRgmjmac
        public async void PlayAudioFile(string filename)
        {
            StorageFolder AssetsFolder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await AssetsFolder.GetFileAsync(filename);

            var player = new MediaPlayer()
            {
                AutoPlay = false,
                Source = MediaSource.CreateFromStorageFile(file),
            };

            player.Play();
        }
    }
}

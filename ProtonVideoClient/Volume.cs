using AudioSwitcher.AudioApi.CoreAudio;

namespace ProtonVideoClient
{
    internal static class Volume
    {
        internal static double _oldVolume;

        internal static void SetNewVolume(byte volume)
        {
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            _oldVolume = defaultPlaybackDevice.Volume;
            defaultPlaybackDevice.Volume = volume;
        }

        internal static void RestoreVolume()
        {
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume = _oldVolume;
        }
    }
}

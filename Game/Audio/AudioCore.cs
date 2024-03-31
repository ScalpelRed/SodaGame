using Triode.Game.General;

namespace Triode.Game.Audio
{
    public class AudioCore
    {

        private readonly SoLoud.SoLoud Soloud;

        public readonly GameCore GameCore;

        public float MainVolume = 1;

        public AudioCore(GameCore gameCore)
        {
            GameCore = gameCore;
            Soloud = new SoLoud.SoLoud();
            Soloud.init();
        }

        ~AudioCore()
        {
            Soloud.deinit();
        }

        public void Play(Sound sound, float volume = 1f)
        {
            Soloud.play(sound.SoundData, volume);
        }
    }
}

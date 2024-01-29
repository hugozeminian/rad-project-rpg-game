using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace carrot_game
{
    /// <summary>
    /// Game audios.
    /// </summary>
    class Audio
    {

        // Create AudioFileReader soundGame
        //public AudioFileReader audioIntro;
        public AudioFileReader AudioMenu { get; private set; } = new AudioFileReader("Resources\\menu.wav");
        
        public AudioFileReader AudioBackgroundPhase1 { get; private set; } = new AudioFileReader("Resources\\up_down.wav");
        
        public AudioFileReader AudioHeroWalkingInGrass { get; private set; } = new AudioFileReader("Resources\\hero-walking-grass-side.wav");
        public AudioFileReader AudioHeroAttack {  get; private set; } = new AudioFileReader("Resources\\mixkit-karate-fighter-hit-2154.wav");

        public AudioFileReader AudioMonsterBatAttack { get; private set; } = new AudioFileReader("Resources\\mixkit-falling-on-undergrowth-390.wav");
        public AudioFileReader AudioMonsterSpiderAttack { get; private set; } = new AudioFileReader("Resources\\mixkit-hard-and-quick-punch-2143.wav");
        public AudioFileReader AudioMonsterBunnyAttack { get; private set; } = new AudioFileReader("Resources\\mixkit-body-punch-quick-hit-2153.wav");
        public AudioFileReader AudioMonsterBlackBunnyAttack { get; private set; } = new AudioFileReader("Resources\\mixkit-martial-arts-fast-punch-2047.wav");

        public AudioFileReader AudioItemCarrotCollected { get; private set; } = new AudioFileReader("Resources\\mixkit-player-jumping-in-a-video-game-2043.wav");
        public AudioFileReader AudioDoor { get; private set; } = new AudioFileReader("Resources\\mixkit-automatic-door-shut-204.wav");



        // Create a WaveOutEvent instance for audio playback
        private WaveOutEvent WaveOutBackground = new WaveOutEvent();
        
        private WaveOutEvent WaveOutHeroWalkingInGrass = new WaveOutEvent();
        private WaveOutEvent WaveOutHeroAttack = new WaveOutEvent();

        private WaveOutEvent WaveOutMonsterBatAttack = new WaveOutEvent();
        private WaveOutEvent WaveOutMonsterSpiderAttack = new WaveOutEvent();
        private WaveOutEvent WaveOutMonsterBunnyAttack = new WaveOutEvent();
        private WaveOutEvent WaveOutMonsterBlackBunnyAttack = new WaveOutEvent();

        private WaveOutEvent WaveOutItemCarrotCollected = new WaveOutEvent();
        private WaveOutEvent WaveOutAudioDoor = new WaveOutEvent();



        // ############### AUDIO FUNCTIONS ###############

        //Background music
        private bool isBackgroundAudioLooping = true;
        public void PlayAudioBackgroud(AudioFileReader audio)
        {
            audio.Volume = 0.1f;
            WaveOutBackground.Init(audio);

            WaveOutBackground.PlaybackStopped += (sender, e) => // Loop
            {
                if (e.Exception != null) 
                {
                    // Handle the error - ToDo
                }
                else if(isBackgroundAudioLooping)
                {
                    audio.CurrentTime = TimeSpan.Zero;
                    WaveOutBackground.Play();
                }
            };
            isBackgroundAudioLooping = true;
            WaveOutBackground.Play();
        }
        public void StopAudioBackgroud()
        {
            isBackgroundAudioLooping = false;
            WaveOutBackground.Stop();
        }

        //Sound Effect
        private void PlaySoundEffect(WaveOutEvent waveOut, AudioFileReader audioEffect)
        {
            audioEffect.Volume = 0.2f;

            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = new WaveOutEvent();
            }

            waveOut.Init(audioEffect);
            audioEffect.CurrentTime = TimeSpan.Zero;
            waveOut.Play();
        }

        //Player Effects
        public void PlayHeroWalkingSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutHeroWalkingInGrass, audioEffect);
        }

        public void PlayHeroAttackSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutHeroAttack, audioEffect);
        }


        //Monster Effects
        public void PlayMonsterBatAttackSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutMonsterBatAttack, audioEffect);
        }

        public void PlayMonsterSpiderAttackSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutMonsterSpiderAttack, audioEffect);
        }

        public void PlayMonsterBunnyAttackSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutMonsterBunnyAttack, audioEffect);
        }

        public void PlayMonsterBlackBunnyAttackSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutMonsterBlackBunnyAttack, audioEffect);
        }

        // Other effects
        public void PlayItemCarrotCollectedSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutItemCarrotCollected, audioEffect);
        }

        public void PlayDoorSoundEffect(AudioFileReader audioEffect)
        {
            PlaySoundEffect(WaveOutAudioDoor, audioEffect);
        }


        //All disposes
        public void Dispose()
        {
            WaveOutBackground.Dispose();
            WaveOutHeroWalkingInGrass.Dispose();
            WaveOutHeroAttack.Dispose();
            WaveOutMonsterBatAttack.Dispose();
            WaveOutMonsterSpiderAttack.Dispose();
            WaveOutMonsterBunnyAttack.Dispose();
            WaveOutMonsterBlackBunnyAttack.Dispose();
            WaveOutItemCarrotCollected.Dispose();
            WaveOutAudioDoor.Dispose();

            AudioMenu.Dispose();
            AudioBackgroundPhase1.Dispose();
            AudioHeroWalkingInGrass.Dispose();
            AudioHeroAttack.Dispose();
            AudioMonsterBatAttack.Dispose();
            AudioMonsterBunnyAttack.Dispose();
            AudioMonsterBlackBunnyAttack.Dispose();
            AudioItemCarrotCollected.Dispose();
            AudioDoor.Dispose();
        }
    }
}

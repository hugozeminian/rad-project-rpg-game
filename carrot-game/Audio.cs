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

        // Create a WaveOutEvent instance for audio playback
        private WaveOutEvent WaveOutBackground = new WaveOutEvent();
        private WaveOutEvent WaveOutHeroWalkingInGrass = new WaveOutEvent();

        // toDo


        // ############### AUDIO FUNCTIONS ###############

        //Background music
        private bool isBackgroundAudioLooping = true;
        public void PlayAudioBackgroud(AudioFileReader audio)
        {
            audio.Volume = 1.0f;
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


        //All disposes
        public void Dispose()
        {
            WaveOutBackground.Dispose();
            WaveOutHeroWalkingInGrass.Dispose();

            //audioIntro.Dispose();
            AudioMenu.Dispose();
            AudioBackgroundPhase1.Dispose();
            AudioHeroWalkingInGrass.Dispose();
        }
    }
}

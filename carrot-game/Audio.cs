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
        public AudioFileReader audioMenu { get; private set; } = new AudioFileReader("Resources\\menu.wav");
        public AudioFileReader audioBackgroundPhase1 { get; private set; } = new AudioFileReader("Resources\\up_down.wav");
        public AudioFileReader audioHeroWalkingInGrass { get; private set; } = new AudioFileReader("Resources\\hero-walking-grass-side.wav");

        // Create a WaveOutEvent instance for audio playback
        private WaveOutEvent waveOutBackground = new WaveOutEvent();
        private WaveOutEvent waveOutHeroWalkingInGrass = new WaveOutEvent();

        // toDo


        // ############### AUDIO FUNCTIONS ###############

        //Background music
        private bool isBackgroundAudioLooping = true;
        public void playAudioBackgroud(AudioFileReader audio)
        {
            audio.Volume = 1.0f;
            waveOutBackground.Init(audio);

            waveOutBackground.PlaybackStopped += (sender, e) => // Loop
            {
                if (e.Exception != null) 
                {
                    // Handle the error - ToDo
                }
                else if(isBackgroundAudioLooping)
                {
                    audio.CurrentTime = TimeSpan.Zero;
                    waveOutBackground.Play();
                }
            };
            isBackgroundAudioLooping = true;
            waveOutBackground.Play();
        }
        public void stopAudioBackgroud()
        {
            isBackgroundAudioLooping = false;
            waveOutBackground.Stop();
        }


        //All disposes
        public void Dispose()
        {
            waveOutBackground.Dispose();
            waveOutHeroWalkingInGrass.Dispose();

            //audioIntro.Dispose();
            audioMenu.Dispose();
            audioBackgroundPhase1.Dispose();
            audioHeroWalkingInGrass.Dispose();
        }
    }
}

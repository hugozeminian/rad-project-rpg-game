using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace carrot_game
{
    public partial class GameScreen : Form
    {
        Player heroCharacter = new Player();

        // Declaring a refresh rate of 30 frames per second [33.33ms] (1000ms / 30)
        public static int fps = 30;
        public static int refreshTime = 1000/fps;

        Audio bgm = new Audio();

        public GameScreen()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = new Size(1920, 1080);
            bgm.playAudioBackgroud(bgm.audioBackgroundPhase1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Refresh redraws everything in the form.
            Refresh();
            // updates the character's position and sprite image.
            heroCharacter.Update();

        }

        private void InitializeObjects()
        {
            heroCharacter = new Player();
        }

        private void PaintObjects(object sender, PaintEventArgs e)
        {
            // Create a Graphics object to draw on the form
            Graphics g = e.Graphics;

                // Draw the image at the character's position (PosX, PosY)
                g.DrawImage(heroCharacter.CurrentSprite, heroCharacter.PosX, heroCharacter.PosY, heroCharacter.Width, heroCharacter.Height);
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            InitializeObjects();

            // Add a Paint event handler
            this.Paint += new PaintEventHandler(this.PaintObjects);

            // Start the timer for redrawing
            timer1.Start();
        }

        private void GameScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            KeyHandler.HandleKeyDown(e, heroCharacter);
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            KeyHandler.HandleKeyRelease(e, heroCharacter);
        }
    }
}

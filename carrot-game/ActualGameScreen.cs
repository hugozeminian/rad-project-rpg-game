using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carrot_game
{
    public partial class ActualGameScreen : Form
    {
        Player heroCharacter;

        // Declaring a refresh rate of 60 frames per second (1000ms / 60)
        public static int fps = 1000/60;
        public ActualGameScreen()
        {
            InitializeComponent();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this redraws everything in the form
            this.Refresh();
            //this.Invalidate();
        }



        private void InitializeObjects()
        {
            heroCharacter = new Player();
        }

        private void PaintObjects(object sender, PaintEventArgs e)
        {
            // Create a Graphics object to draw on the form
            Graphics g = e.Graphics;

            // Loop through the player character's sprite images and draw them
            foreach (Bitmap spriteImage in heroCharacter.SpriteImages)
            {
                // Draw the image at the character's position (PosX, PosY)
                g.DrawImage(spriteImage, heroCharacter.PosX, heroCharacter.PosY, heroCharacter.Width, heroCharacter.Height);
            }

            // Dispose of the Graphics object
            g.Dispose();
        }

        private void ActualGameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            heroCharacter.HandleKeyPress(e);
        }





        private void ActualGameScreen_Load(object sender, EventArgs e)
        {
            InitializeObjects();

            // Add a Paint event handler
            this.Paint += new PaintEventHandler(this.PaintObjects);

            // Add a KeyDown event handler to handle key presses
            this.KeyDown += new KeyEventHandler(ActualGameScreen_KeyDown);

            // Start the timer for redrawing
            timer1.Start();
        }
}
}

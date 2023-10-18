using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carrot_game
{
    /// <summary>
    /// Player Character - 
    /// </summary>
    class Player : Entity
    {
        public string Name;
        // Controls how many pixels we move per Move()
        public int HealthPoints { get; set; }
        // Our x-axis position, in pixels
        public int ExperiencePoints { get; set; }
        // Our y-axis position, in pixels
        public int Attack { get; set; }
        // Our character's width, in pixels
        public int Speed { get; set; }
        // Our character's height, in pixels
        public int Defense { get; set; }
        // The path for the current sprite (image)
        public int PosX { get; set; }
        // Identifies which direction the character is facing
        public int PosY { get; set; }//(up, down, left, right)
        public int PosZ { get; set; }
        public string[] SpriteImages { get; set; }
        public int Carrots { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }


        public void HandleKeyPress(KeyEventArgs e)
            {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                Move(0, -Speed, 0);
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                Move(0, Speed, 0);
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                Move(0, -Speed, 0);
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                Move(0, Speed, 0);
            }
        }   

        public void Move(int x, int y, int z)
        {
            PosX += x;
            PosY += y;
            PosZ += z;
        }

        public Player()
        {
            Name = "Player";
            Speed = 4;
            PosX = 100; // ToDo - Change to middle of the screen
            PosY = 100; // ToDo - Change to middle of the screen
            Width = 64;
            Height = 64;
            SpriteImages.Append("Resources\\front2.bmp");
        }

        public void ChangeName(string name)
        {

        }

        // Add other Move directions

        // Add method LevelUp

        // add method gain exp

        // add method attack

        // add method interact

    }
}

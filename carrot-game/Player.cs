using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carrot_game
{
    /// <summary>
    /// Player Character - 
    /// </summary>
    class Player : Entity
    {
        public string Name;
        // Controls how many pixels we move per Move()
        public int Speed;
        // Our x-axis position, in pixels
        public int Pos_x;
        // Our y-axis position, in pixels
        public int Pos_y;
        // Our character's width, in pixels
        public int Width;
        // Our character's height, in pixels
        public int Height;
        // The path for the current sprite (image)
        public string Sprite;
        // Identifies which direction the character is facing
        public string Direction;//(up, down, left, right)

        public void MoveUp()
            {
                // ToDo - check if we are inside the screen boundaries before moving!
                //if yes,
                Pos_y -= Speed;
                Direction = "up";
            }   

        public Player()
        {
            Name = "Player";
            Speed = 4;
            Pos_x = 100; // ToDo - Change to middle of the screen
            Pos_y = 100; // ToDo - Change to middle of the screen
            Width = 64;
            Height = 64;
            Sprite = "res\\image\\entity\\character\\front2.png";
        }

        public void ChangeName()
        {

        }

        // Add other Move directions

        // Add method LevelUp

        // add method gain exp

        // add method attack

        // add method interact

    }
}

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
    /// Entities are 'living' things. Our character and monster classes will inherit this. Entities have: Name, Health Points, Experience Points, Attack, Speed, Defense and SpriteImage. They can Attack(), Move(), Die()
    /// </summary>
    abstract class Entity
    {
        public string Name { get; set; }
        public int MaxHealthPoints { get; set; }
        public int CurrentHealthPoints { get; set; }
        public int ExperiencePoints { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }
        public int Defense { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int PosZ { get; set; }
        public string Direction { get; set; }
        public Bitmap[,] SpriteImages;
        public int Carrots { get; set; }
        public int Width {  get; set; }
        public int Height { get; set; }
        public int Sprite = 0;

        public bool CanMoveUp, CanMoveDown, CanMoveLeft, CanMoveRight;
        public bool UpPressed, DownPressed, LeftPressed, RightPressed;

        /// <summary>
        /// Adjust this according to the <see cref="Monster"/ or <see cref="Player"/'s geometry.>>
        /// </summary>
        public abstract Rectangle BoundingBox { get;}        
        
        public int FrameCounter { get; set; }

        public abstract void Move(int x, int y, int z);

        public void DisableMovement()
        {
            RightPressed = LeftPressed = UpPressed = DownPressed = false;
            CanMoveDown = CanMoveLeft = CanMoveRight = CanMoveUp = false;
        }

        public void AllowMovement()
        {
            CanMoveDown = CanMoveLeft = CanMoveRight = CanMoveUp = true;
        }
        //Entities are abstract classes as we won't have any instantiated Entities. What we will have are monsters and a player, which must implement this abstract class.
    }
}

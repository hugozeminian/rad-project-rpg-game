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
        public abstract string Name { get; set; }
        public abstract int HealthPoints { get; set; }
        public abstract int ExperiencePoints { get; set; }
        public abstract int Attack { get; set; }
        public abstract int Speed { get; set; }
        public abstract int Defense { get; set; }
        public abstract int PosX { get; set; }
        public abstract int PosY { get; set; }
        public abstract int PosZ { get; set; }
        public abstract string Direction { get; set; }
        public abstract Bitmap[,] SpriteImages { get; set; }
        public abstract int Carrots { get; set; }
        public abstract int Width {  get; set; }
        public abstract int Height { get; set; }
        public int Sprite = 0;

        public abstract int FrameCounter { get; set; }

        public abstract void Move(int x, int y, int z);

        //Entities are abstract classes as we won't have any instantiated Entities. What we will have are monsters and a player, which must implement this abstract class.
    }
}

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
        public int HealthPoints { get; set; } = 3;
        public int ExperiencePoints { get; set; } = 0;
        public int Attack { get; set; } = 1;
        public int Speed { get; set; } = 5;
        public int Defense { get; set; } = 1;
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int PosZ { get; set; }
        public Bitmap[] SpriteImages { get; set; }
        public int Carrots { get; set; }
        public int Width {  get; set; }
        public int Height { get; set; }

        public abstract void HandleKeyPress(KeyEventArgs e);

        //        if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
        //        {
        //            Move(0, -Speed, 0);
        //    }
        //        if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
        //        {
        //            Move(0, Speed, 0);
        //}
        //if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
        //{
        //    Move(0, -Speed, 0);
        //}
        //if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
        //{
        //    Move(0, Speed, 0);
        //}


        public abstract void Move(int x, int y, int z);
        //{
        //    PosX += x;
        //    PosY += y;
        //    PosZ += z;
        //}

        //Entities are abstract classes as we won't have any instantiated Entities. What we will have are monsters and a player, which must implement this abstract class.
    }
}

﻿using System;
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
        public bool ShowBoundingBox {get;set;} = false;
        public bool ShowName { get;set;} = false;
        public abstract Rectangle BoundingBox { get;}        
        
        public int FrameCounter { get; set; }

        public abstract void Move(int x, int y, int z);

        //Entities are abstract classes as we won't have any instantiated Entities. What we will have are monsters and a player, which must implement this abstract class.
    }
}

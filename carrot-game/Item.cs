using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace carrot_game
{
    /// <summary>
    /// Item - 
    /// </summary>
    class Item
    {
        public Image CarrotImage = Properties.Resources.carrot;

        Audio ItemSoundEffect = new Audio();

        public int ScreenX { get; set; }
        public int ScreenY { get; set; }
        public int Width { get; set; } = 80;
        public int Height { get; set; } = 80;
        public bool IsCollected { get; set; }

        private GameScreen gameScreen;


        public Item(int x, int y)
        {
            ScreenX = x;
            ScreenY = y;
            IsCollected = false;
        }

        public static Item SpawnCarrot(int screenWidth, int screenHeight)
        {
            var random = new Random();
            int x = random.Next(0, screenWidth - Properties.Resources.carrot.Width);
            int y = random.Next(0, screenHeight - Properties.Resources.carrot.Height);

            return new Item(x, y);
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(ScreenX, ScreenY, Width, Height);
            }
        }

        public void ItemCarrotCollected()
        {
            ItemSoundEffect.PlayItemCarrotCollectedSoundEffect(ItemSoundEffect.AudioItemCarrotCollected);

        }
    }
}

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

        public int ScreenX { 
            get {
                return WorldX - GameScreen.gs.player.WorldX + GameScreen.gs.player.ScreenX;
            }
            set {
                ScreenX = value;
            } 
        }
        public int ScreenY
        {
            get
            {
                return WorldY - GameScreen.gs.player.WorldY + GameScreen.gs.player.ScreenY;
            }
            set
            {
                ScreenY = value;
            }
        }
        public int WorldX;
        public int WorldY;
        public int Width { get; set; } = 80;
        public int Height { get; set; } = 80;
        public bool IsCollected { get; set; }

        private GameScreen gameScreen;


        public Item(int x, int y)
        {
            WorldX = x;
            WorldY = y;
            IsCollected = false;
        }

        public static Item SpawnCarrot(Map map)
        {
            Random _r = new Random();
            int row = 0;
            int col = 0;
            while (GameScreen.gs.gameMap.mapArray[row,col].collision != false)
            {
                row = _r.Next(0, GameScreen.gs.gameMap.mapData.GetLength(0));
                col = _r.Next(0, GameScreen.gs.gameMap.mapData.GetLength(1));
            }

            return new Item(row * MapTile.tileSize, col * MapTile.tileSize);
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

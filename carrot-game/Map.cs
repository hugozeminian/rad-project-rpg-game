using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace carrot_game
{
    internal class Map
    {
        private List<Bitmap> grassSprites;
        private List<Point> grassPositions;
        private int screenWidth;
        private int screenHeight;

        public Map(int width, int height)
        {
            grassSprites = new List<Bitmap>();
            grassPositions = new List<Point>();
            screenWidth = width;
            screenHeight = height;

            LoadGrassSprites();
        }

        private void LoadGrassSprites()
        {
            int grassCount = 5; // Number of grass sprites
            int margin = 50; // Margin from screen edge

            for (int i = 0; i < grassCount; i++)
            {
                // Calculate grass sprite positions based on screen size and margin
                int x = (i + 1) * (screenWidth - 2 * margin) / (grassCount + 1) + margin;
                int y = screenHeight - margin;

                Bitmap grassBitmap = new Bitmap(Properties.Resources.grass1);
                grassSprites.Add(grassBitmap);

                grassPositions.Add(new Point(x, y - grassBitmap.Height));
            }
        }

        public List<Bitmap> GetGrassSprites()
        {
            return grassSprites;
        }

        public List<Point> GetGrassPositions()
        {
            return grassPositions;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace carrot_game
{
    /// <summary>
    /// Map - 
    /// </summary>
    internal class Map
    {
 
        private List<Image> tileImages;
        private int tileSize = 64; // Tile size in pixels
        private int[,] mapData;

        public Map(int[,] mapData)
        {
            this.mapData = mapData;
            LoadTileImages();
        }

        // Load tile images from resources
        private void LoadTileImages()
        {
            // Load tile images
            tileImages = new List<Image>
            {
                Properties.Resources.grass1, //1
            
                Properties.Resources.water1, //2
                Properties.Resources.water2, //3
                Properties.Resources.water3, //4
                Properties.Resources.water4, //5
                Properties.Resources.water5, //6

                Properties.Resources.GrassBottomLeft_WaterTopRight, //7
                Properties.Resources.GrassBottomRight_WaterTopLeft, //8
                Properties.Resources.GrassBottom_WaterAround, //9
                Properties.Resources.GrassBottom_WaterTop, //10
                Properties.Resources.GrassLeft_WaterAround, //11
                Properties.Resources.GrassLeft_WaterRight, //12
                Properties.Resources.GrassRight_WaterAround, //13
                Properties.Resources.GrassRight_WaterLeft, //14
                Properties.Resources.GrassTopLeft_WaterBottomRight, //15
                Properties.Resources.GrassTopRight_WaterBottomLeft, //16
                Properties.Resources.GrassTop_WaterAround, //17
                Properties.Resources.GrassTop_WaterBottom, //18

                Properties.Resources.wall, //19
                Properties.Resources.House, //20
            };
        }

        // Draw the map based on the mapData
        public void Draw(Graphics g)
        {
            for (int row = 0; row < mapData.GetLength(0); row++)
            {
                for (int col = 0; col < mapData.GetLength(1); col++)
                {
                    int tileType = mapData[row, col];
                    if (tileType >= 1 && tileType <= tileImages.Count)
                    {
                        // Draw the tile image at the specified position
                        Image tileImage = tileImages[tileType - 1];
                        if(tileType == 20)
                        {
                            g.DrawImage(tileImage, col * tileSize, -tileSize, tileSize*4, tileSize*4);
                        }
                        else
                            g.DrawImage(tileImage, col * tileSize, row * tileSize, tileSize, tileSize);
                    }
                }
            }
        }

    }
}

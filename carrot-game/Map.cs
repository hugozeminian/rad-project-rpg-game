﻿using NAudio.MediaFoundation;
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
        public MapTile[,] mapArray;
        public int[,] mapData;

        public Map()
        {
            mapData = GenerateMap();
        }

        // Load tile images from resources
        private int[,] GenerateMap()
        {
           int[,] _m = 
           {
                { 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18, 18, 18, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 17, 17,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 17, 17,  0, 11,  1,  4, 13,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 11,  3,  2, 13, 11,  4,  5, 15,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 11,  1,  2, 15, 14,  3,  1,  3, 13,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 11,  4,  3,  4,  5,  2,  3,  2, 13,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  6,  1,  2,  1,  1,  4,  1, 15,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  6,  3,  1,  3,  1,  2,  4, 13,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 11,  1,  5,  2,  2,  3,  5, 13,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  6,  1,  8,  2,  1,  7,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  9,  0,  9,  9,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,   0,  0, 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18},
                { 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18}
           };

            int rows = _m.GetLength(0);
            int cols = _m.GetLength(1);

            mapArray = new MapTile[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    MapTile _mt = new MapTile();
                    mapArray[i, j] = _mt;
                    _mt.collision = MapTile.CheckCollision(_m[i, j]);
                    _mt.img = MapTile.TileSet[_m[i, j]].img;
                }
            }
            return _m;
        }



        // Draw the map based on the mapData
        public void Draw(Graphics g, GameScreen gs)
        {
            for (int row = 0; row < mapData.GetLength(0); row++)
            {
                for (int col = 0; col < mapData.GetLength(1); col++)
                {
                    int tileType = mapData[row, col];
                    if (tileType >= 0 && tileType <= MapTile.TileSet.Length)
                    {
                        Image tileImage = MapTile.TileSet[tileType].img;

                        int worldX = col * MapTile.tileSize;
                        int worldY = row * MapTile.tileSize;
                        int screenX = worldX - gs.player.WorldX + gs.player.ScreenX;
                        int screenY = worldY - gs.player.WorldY + gs.player.ScreenY;

                        // Draw the tile image at the specified position
                        if (tileType != 20)
                        {
                            g.DrawImage(tileImage, screenX, screenY, MapTile.tileSize, MapTile.tileSize);
                        }
                        // Special case (House) [TODO: change to entity later]:
                        else
                        {
                            g.DrawImage(tileImage, col * MapTile.tileSize, -MapTile.tileSize, MapTile.tileSize *4, MapTile.tileSize *4);
                        }
                    }
                }
            }
        }
    }
}

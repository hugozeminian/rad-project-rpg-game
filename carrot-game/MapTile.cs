using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carrot_game
{
    internal class MapTile
    {
        public bool collision = false;
        public static int tileSize = 64;
        public Image img; 
        internal static int _arraySize = 20;
        internal static readonly MapTile[] TileImages = new MapTile[_arraySize];
            
        internal static void PopulateArray()
        {
            for (int i = 0; i < _arraySize; i++)
            {
                TileImages[i] = new MapTile();
            }

            TileImages[0].img = Properties.Resources.grass1; //1
            
            TileImages[1].img = Properties.Resources.water1; //2
            TileImages[1].collision = true;

            TileImages[2].img = Properties.Resources.water2; //3
            TileImages[2].collision = true;

            TileImages[3].img = Properties.Resources.water3; //4
            TileImages[3].collision = true;

            TileImages[4].img = Properties.Resources.water4; //5
            TileImages[4].collision = true;

            TileImages[5].img = Properties.Resources.water5; //6
            TileImages[5].collision = true;

            TileImages[6].img = Properties.Resources.GrassBottomLeft_WaterTopRight; //7
            TileImages[7].img = Properties.Resources.GrassBottomRight_WaterTopLeft; //8
            TileImages[8].img = Properties.Resources.GrassBottom_WaterAround; //9
            TileImages[9].img = Properties.Resources.GrassBottom_WaterTop; //10
            TileImages[10].img = Properties.Resources.GrassLeft_WaterAround; //11
            TileImages[11].img = Properties.Resources.GrassLeft_WaterRight; //12
            TileImages[12].img = Properties.Resources.GrassRight_WaterAround; //13
            TileImages[13].img = Properties.Resources.GrassRight_WaterLeft; //14
            TileImages[14].img = Properties.Resources.GrassTopLeft_WaterBottomRight; //15
            TileImages[15].img = Properties.Resources.GrassTopRight_WaterBottomLeft; //16
            TileImages[16].img = Properties.Resources.GrassTop_WaterAround; //17
            TileImages[17].img = Properties.Resources.GrassTop_WaterBottom; //18

            TileImages[18].img = Properties.Resources.wall; //19
            TileImages[18].collision = true;

            TileImages[19].img = Properties.Resources.House; //20
            TileImages[19].collision = true;
        }
    }
}

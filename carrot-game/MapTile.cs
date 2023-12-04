using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace carrot_game
{
    internal class MapTile
    {
        public bool collision = false;
        public static int tileSize = 64;
        public Image img;
        internal static MapTile[] TileSet;

        static MapTile()
        {
            InitializeTileSet();
        }

        public MapTile(bool collision, Image img) : base()
        {
            this.collision = collision;
            this.img = img;
        }
        public MapTile()
        {
        }

        private static void InitializeTileSet()
        {
            TileSet = new MapTile[20]
            {
            new MapTile { img = Properties.Resources.grass },   // 0
            new MapTile { img = Properties.Resources.water1 },  // 1
            new MapTile { img = Properties.Resources.water2 },  // 2
            new MapTile { img = Properties.Resources.water3 },  // 3
            new MapTile { img = Properties.Resources.water4 },  // 4
            new MapTile { img = Properties.Resources.water5 },  // 5

            new MapTile { img = Properties.Resources.GrassBottomLeft_WaterTopRight },   // 6
            new MapTile { img = Properties.Resources.GrassBottomRight_WaterTopLeft },   // 7
            new MapTile { img = Properties.Resources.GrassBottom_WaterAround },         // 8
            new MapTile { img = Properties.Resources.GrassBottom_WaterTop },            // 9
            new MapTile { img = Properties.Resources.GrassLeft_WaterAround },           // 10
            new MapTile { img = Properties.Resources.GrassLeft_WaterRight },            // 11
            new MapTile { img = Properties.Resources.GrassRight_WaterAround },          // 12
            new MapTile { img = Properties.Resources.GrassRight_WaterLeft },            // 13
            new MapTile { img = Properties.Resources.GrassTopLeft_WaterBottomRight },   // 14
            new MapTile { img = Properties.Resources.GrassTopRight_WaterBottomLeft },   // 15
            new MapTile { img = Properties.Resources.GrassTop_WaterAround },            // 16
            new MapTile { img = Properties.Resources.GrassTop_WaterBottom },            // 17

            new MapTile { img = Properties.Resources.wall },                            // 18
            new MapTile { img = Properties.Resources.House }                            // 19
            };

            for (int i = 0; i < TileSet.Length; i++)
            {
                TileSet[i].collision = CheckCollision(i);
            }
        }

            public static bool CheckCollision(int i)
        {
            switch (i)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 18:
                case 19:
                    return true;
                default: return false;
            }
        }
    }
}

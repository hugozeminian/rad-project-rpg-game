using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace carrot_game
{
    internal class WhiteBunny : Monster
    {
        public WhiteBunny() {
            Counter++;
            Bitmap up1 = Properties.Resources.WhiteBunny1;
            Bitmap up2 = Properties.Resources.WhiteBunny2;
            Bitmap up3 = Properties.Resources.WhiteBunny1;
            Bitmap up4 = Properties.Resources.WhiteBunny2;
            Bitmap down1 = Properties.Resources.WhiteBunny1;
            Bitmap down2 = Properties.Resources.WhiteBunny2;
            Bitmap down3 = Properties.Resources.WhiteBunny1;
            Bitmap down4 = Properties.Resources.WhiteBunny2;
            Bitmap left1 = Properties.Resources.WhiteBunny1;
            Bitmap left2 = Properties.Resources.WhiteBunny2;
            Bitmap left3 = Properties.Resources.WhiteBunny1;
            Bitmap left4 = Properties.Resources.WhiteBunny2;
            Bitmap right1 = Properties.Resources.WhiteBunny1;
            Bitmap right2 = Properties.Resources.WhiteBunny2;
            Bitmap right3 = Properties.Resources.WhiteBunny1;
            Bitmap right4 = Properties.Resources.WhiteBunny2;

            Name = "White Bunny";
            ExperiencePoints = 5;
            PosX = 200;
            PosY = 200;
            CurrentSprite = Properties.Resources.WhiteBunny1;
            SpriteImages = new Bitmap[,] {
                { up1, up2, up3, up4 },
                { down1, down2, down3, down4 },
                { left1, left2, left3, left4 },
                { right1, right2, right3, right4 }
            };
        }
    }
}

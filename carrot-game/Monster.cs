using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carrot_game
{
    /// <summary>
    /// A monster is a computer-controlled entity that damages the player when they collide.
    /// </summary>
    class Monster : Entity
    {
        internal static int Counter = 0;
        public override string Name { get; set; } = "Monster";
        public override int MaxHealthPoints { get; set; }
        public override int CurrentHealthPoints { get; set; } = 1;
        public override int ExperiencePoints { get; set; } = 1;

        public override int Attack { get; set; } = 1;
        public override int Defense { get; set; } = 0;
        public override int Speed { get; set; } = 3;
        public override int PosX { get; set; } = 1000;
        public override int PosY { get; set; } = 1000;
        public override int PosZ { get; set; } = 0;
        public override string Direction { get; set; } = "down";
        // This is a placeholder to receive new outfits in the future:
        public string ImgPack { get; set; } = "";

        // A 2D array, each level corresponding to a direction and their respective images:
        // [up[], down[], left[], right[]]
        public Bitmap[,] SpriteImages;

        public Bitmap CurrentSprite;
        public override int Carrots { get; set; } = 1;
        // Player character's size, in pixels.
        public override int Height { get; set; } = 64;
        public override int Width { get; set; } = 64;

        // Controls the current sprite image, to avoid rapidly changing images.
        public override int FrameCounter { get; set; } = 0;

        // Controls wether or not action keys are pressed.
        public bool UpPressed, DownPressed, LeftPressed, RightPressed;

        // Move the character on X, Y and Z axis.
        public override void Move(int x, int y, int z)
        {
            PosX += x;
            PosY += y;
            PosZ += z;
        }

        public void FollowPlayer(Player p)
        {
            if (p.PosX > this.PosX - Width)
            {
                RightPressed = true;
                LeftPressed = false;
            }
            if (p.PosX + p.Width < this.PosX)
            {
                LeftPressed = true;
                RightPressed = false;
            }
            if (p.PosY > this.PosY + Height)
            {
                DownPressed = true;
                UpPressed = false;

            }
            if (p.PosY + p.Height < this.PosY)
            {
                UpPressed = true;
                DownPressed = false;

            }
        }
        public void Update()
        {
            // if movement keys are pressed, move and change direction
            if (UpPressed || DownPressed || LeftPressed || RightPressed)
            {
                if (UpPressed)
                {
                    Move(0, -Speed, 0);
                    Direction = "up";
                }
                if (DownPressed)
                {
                    Move(0, Speed, 0);
                    Direction = "down";
                }
                if (LeftPressed)
                {
                    Move(-Speed, 0, 0);
                    Direction = "left";
                }
                if (RightPressed)
                {
                    Move(Speed, 0, 0);
                    Direction = "right";
                }

                // adds a counter to our movement frame counter
                FrameCounter++;

                // once we reach this threshold, we change the image to the next available in the array.
                if (FrameCounter > 9)
                {
                    if (Sprite == 0)
                    {
                        Sprite = 1;
                    }
                    else if (Sprite == 1)
                    {
                        Sprite = 2;
                    }
                    else if (Sprite == 2)
                    {
                        Sprite = 3;
                    }
                    else if (Sprite == 3)
                    {
                        Sprite = 0;
                    }
                    FrameCounter = 0;
                }

                // this defines the image to be displayed
                switch (Direction)
                {
                    case "up":
                        CurrentSprite = SpriteImages[0, Sprite];
                        break;
                    case "down":
                        CurrentSprite = SpriteImages[1, Sprite];
                        break;
                    case "left":
                        CurrentSprite = SpriteImages[2, Sprite];
                        break;
                    case "right":
                        CurrentSprite = SpriteImages[3, Sprite];
                        break;
                }
            }
        }
    }
    
}

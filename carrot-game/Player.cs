using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace carrot_game
{
    /// <summary>
    /// Player Character - 
    /// </summary>
    class Player : Entity
    {
        // Player's name.
        public override string Name { get; set; } = "Player";
        // Player's health points.
        public override int HealthPoints { get; set; } = 1;
        // Player's experience points.
        public override int ExperiencePoints { get; set; } = 0;
        // Player's attack power.
        public override int Attack { get; set; } = 1;
        // Player's Defense points.
        public override int Defense { get; set; } = 0;
        // Player's speed.
        public override int Speed { get; set; } = 5;
        // Player's position in the X axis.
        public override int PosX { get; set; } = 100;
        // Player's position in the Y axis.
        public override int PosY { get; set; } = 100;
        // Player's position in the Z axis.
        public override int PosZ { get; set; } = 0;
        // Player's sprite collection to animate movement:
        public override string Direction {get; set; } = "down";
        // A 2D array, each level corresponding to a direction and their respective images:
        // [up[], down[], left[], right[]]
        public override Bitmap[,] SpriteImages { get; set; }
        public Bitmap CurrentSprite { get; set; }
        // Player's quantity of owned carrots.
        public override int Carrots { get; set; } = 0;
        // Player character's size, in pixels.
        public override int Height { get; set; } = 128;
        // Player character's size, in pixels.
        public override int Width { get; set; } = 128;

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

        public Player()
        {
            Name = "Player";
            Speed = 5;
            PosX = 100; // ToDo - Change to middle of the screen
            PosY = 100; // ToDo - Change to middle of the screen
            Width = 128;
            Height = 128;
            SpriteImages = GetPlayerImages("");
            CurrentSprite = Properties.Resources.front1;
        }

        // Changes the character's name.
        public void ChangeName(string name)
        {
            this.Name = name;
        }

        // Method to handle character changes: Movement, direction and current sprite.
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
                switch(Direction)
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

        // We will use this Method to get the proper collection of images corresponding to the "Image Pack" (or let's call it outfit pack). This way we can change the character's appearance once we add more visual styles.
        public Bitmap[,] GetPlayerImages(string imgPack)
        {
            Bitmap up1 = Properties.Resources.back1;
            Bitmap up2 = Properties.Resources.back2;
            Bitmap up3 = Properties.Resources.back3;
            Bitmap up4 = Properties.Resources.back4;
            Bitmap down1 = Properties.Resources.front1;
            Bitmap down2 = Properties.Resources.front2;
            Bitmap down3 = Properties.Resources.front1;
            Bitmap down4 = Properties.Resources.front2;
            Bitmap left1 = Properties.Resources.left1;
            Bitmap left2 = Properties.Resources.left2;
            Bitmap left3 = Properties.Resources.left3;
            Bitmap left4 = Properties.Resources.left4;
            Bitmap right1 = Properties.Resources.right1;
            Bitmap right2 = Properties.Resources.right2;
            Bitmap right3 = Properties.Resources.right3;
            Bitmap right4 = Properties.Resources.right4;

            // A 2D array, each level corresponding to a direction and their respective images:
            // [up[], down[], left[], right[]]
            return new Bitmap[,] {
                { up1, up2, up3, up4 },
                { down1, down2, down3, down4 },
                { left1, left2, left3, left4 },
                { right1, right2, right3, right4 }
            };
        }

        // Add method LevelUp

        // add method gain exp

        // add method attack

        // add method interact
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace carrot_game
{
    /// <summary>
    /// Player Character - 
    /// </summary>
    class Player : Entity
    {
        public override string Name { get; set; } = "Player";
        public override int MaxHealthPoints { get; set; }
        public override int CurrentHealthPoints { get; set; } = 10;
        public override int ExperiencePoints { get; set; } = 0;
        public int ExpToNextLevel { get; set; } = 100;

        public int Level { get; set; } = 1;
        public override int Attack { get; set; } = 1;
        public override int Defense { get; set; } = 0;
        public override int Speed { get; set; } = 5;
        public override int PosX { get; set; } = 100;
        public override int PosY { get; set; } = 100;
        public override int PosZ { get; set; } = 0;
        public override string Direction {get; set; } = "down";
        // This is a placeholder to receive new outfits in the future:
        public string ImgPack { get; set; } = "";

        // A 2D array, each level corresponding to a direction and their respective images:
        // [up[], down[], left[], right[]]
        public Bitmap[,] SpriteImages;

        public Bitmap CurrentSprite;
        public override int Carrots { get; set; } = 0;
        // Player character's size, in pixels.
        public override int Height { get; set; } = 128;
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
            Speed = 4;
            PosX = 100; // ToDo - Change to middle of the screen
            PosY = 100; // ToDo - Change to middle of the screen
            Width = 128;
            Height = 128;
            SpriteImages = GetPlayerImages(ImgPack);
            CurrentSprite = Properties.Resources.front1;
        }

        // This constructor is used to Load a character from a save file, given the save slot number:
        public Player(int save)
        {
            List<string> lines = new List<string>();

            // This obtains the list of properties from 'this' object (the instance of Player)
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);

            try
            {

                using (StreamReader sr = new StreamReader(File.OpenRead($"save{save}.txt")))
                {
                    while (sr.EndOfStream == false)
                    {
                        string line = sr.ReadLine();
                        if (line != null)
                            lines.Add(line);
                        else continue;
                    }
                    sr.Close();



                    // for every line of our save-file
                    for (int i = 0; i < lines.Count; i++)
                    {
                        // We search the properties list using our property name, obtained from the save file.
                        PropertyDescriptor myProperty = properties.Find($"{lines[i].Split('=')[0]}", false);

                        // If it's a string, we can use the text file to directly assign the value:
                        if (myProperty.PropertyType == typeof(string))
                        myProperty.SetValue(this, lines[i].Split('=')[1]);

                        // if it's not a string, then it's an int value, and we must parse the string to int:
                        else
                        myProperty.SetValue(this, Int32.Parse(lines[i].Split('=')[1]));
                    }

                }
            } catch (FileNotFoundException fnfEx)
            {
            } finally
            {
                // Load the corresponding images
                CurrentSprite = Properties.Resources.front1; // ToDo this will be displaying the default skin before the player moves. --should fix.
                SpriteImages = GetPlayerImages(ImgPack);
            }
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

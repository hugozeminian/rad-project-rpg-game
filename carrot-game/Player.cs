using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace carrot_game
{
    /// <summary>
    /// Player Character - 
    /// </summary>
    class Player : Entity, ICollision
    {
        // Defines the current player instance:
        public static Player currentPlayer;


        // This is the list that stores the damage values the player received and their relative position on top of the player. [dmg, pos].
        // Damage values are added by ResolveAttack and removed after their position reaches a treshold.
        public List<int[]> damageNumbers = new List<int[]>();

        public int ExpToNextLevel { get; set; } = 100;
        public int Level { get; set; } = 1;

        // This is a placeholder to receive new outfits in the future:
        public string ImgPack { get; set; } = "";

        // Controls the current sprite image, to avoid rapidly changing images.
        public Bitmap CurrentSprite;

        public List<Attack> attacks = new List<Attack>();
        public bool IsAttacking = false;
        public int attackFrame = -1;

        //Player sounds
        Audio pSoundEffect = new Audio();

        public override Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(ScreenX + Width/3, ScreenY+ 2*Height / 3, Width * GameScreen.GlobalScale / 6, Height * GameScreen.GlobalScale / 6);
            }
        }

        // Move the character on X, Y and Z axis.
        public override void Move(int x, int y, int z)
        {
            WorldX += x;
            WorldY += y;
            PosZ += z;
        }



        public Player()
        {
            Name = "Player";
            MaxHealthPoints = 10;
            CurrentHealthPoints = MaxHealthPoints;
            ExperiencePoints = 0;
            Attack = 1;
            Defense = 0;
            Speed = 4;
            WorldX = 150 + MapTile.tileSize*13;
            WorldY = 230;
            PosZ = 1;
            Direction = "down";
            Carrots = 0;
            Height = 64 * GameScreen.GlobalScale;
            Width = 64 * GameScreen.GlobalScale;
            FrameCounter = 0;
            SpriteImages = GetPlayerImages(ImgPack);
            CurrentSprite = Properties.Resources.front1;
            currentPlayer = this;
        }

        // This constructor is used to Load a character from a save file, given the save slot number:
        public Player(int save) : base()
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
                        List<string> prop = lines[i].Split('=').ToList();
                        string propName = prop[0];
                        prop.RemoveAt(0);
                        string propValue = string.Join("=", prop);

                        // We search the properties list using our property name, obtained from the save file.
                        PropertyDescriptor myProperty = properties.Find($"{propName}", false);

                            // If it's a string, we can use the text file to directly assign the value:
                            if (myProperty.PropertyType == typeof(string))
                            myProperty.SetValue(this, propValue);

                            // if it's not a string, we must parse the string:
                            else if (myProperty.PropertyType == typeof(int))
                            myProperty.SetValue(this, Int32.Parse(propValue));
                            else if (myProperty.PropertyType == typeof(bool))
                            myProperty.SetValue(this, Boolean.Parse(propValue));
                            else if (myProperty.PropertyType == typeof(Rectangle))
                            {
                            int x = Int32.Parse(propValue.Split(',')[0].Split('=')[1]);
                            int y = Int32.Parse(propValue.Split(',')[1].Split('=')[1]);
                            int w = Int32.Parse(propValue.Split(',')[2].Split('=')[1]);
                            int h = Int32.Parse(propValue.Split(',')[3].Split('=')[1].Replace("}", ""));
                            myProperty.SetValue(this, new Rectangle(x,y,w,h));
                        }
                    }
                }
            } catch (FileNotFoundException fnfEx)
            {
                // Do nothing with the exception for now
            } 

            finally
            {
                // Load the corresponding images
                CurrentSprite = Properties.Resources.front1; // ToDo this will be displaying the default skin before the player moves. --should fix.
                SpriteImages = GetPlayerImages(ImgPack);
            }
        }

        // Changes the character's name.
        public void ChangeName(string name)
        {
            Name = name;
        }

        // Method to handle character changes: Movement, direction and current sprite.
        public void Update()
        {
            // if movement keys are pressed, move and change direction
            if (UpPressed || DownPressed || LeftPressed || RightPressed || IsAttacking)
            {
                if (IsAttacking)
                {
                    attackFrame++;
                    PlayerAttack();
                }
                if (UpPressed)
                {
                    Direction = "up";
                }
                if (DownPressed)
                {
                    Direction = "down";
                }
                if (LeftPressed)
                {
                    Direction = "left";
                }
                if (RightPressed)
                {
                    Direction = "right";
                }

                // Checking for any tile collision
                isColliding = false;
                GameScreen.gs.cChecker.CheckTileCollision(this);

                // Allow movement if no collision:
                 if (!isColliding)
                 {
                    if (LeftPressed)
                        Move(-Speed, 0, 0);
                    if (RightPressed)
                        Move(Speed, 0, 0);
                    if (UpPressed)
                        Move(0, -Speed, 0);
                    if (DownPressed)
                        Move(0, Speed, 0);
                 }

                // adds a counter to our movement frame counter
                FrameCounter++;

                // once we reach this threshold, we change the image to the next available in the array.
                if (FrameCounter > 9 && !IsAttacking)
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
                else if (attackFrame > 3 && IsAttacking)
                {
                    if (Sprite == 0)
                    {
                        Sprite = 1;
                        attackFrame = 0;
                    }
                    else if (Sprite == 1)
                    {
                        Sprite = 2;
                        attackFrame = 0;
                    }
                    else if (Sprite == 2)
                    {
                        Sprite = 3;
                        attackFrame = 0;
                    }
                    else if (Sprite == 3)
                    {
                        Sprite = 0;
                        attackFrame = -1;
                        IsAttacking = false;
                        AllowMovement();
                    }
                }

                // this defines the image to be displayed if not attacking
                if (!IsAttacking)
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
                else if (IsAttacking)
                    switch (Direction)
                    {
                        case "up":
                            CurrentSprite = SpriteImages[4, Sprite];
                        break;
                        case "down":
                            CurrentSprite = SpriteImages[5, Sprite];
                        break;
                        case "left":
                            CurrentSprite = SpriteImages[6, Sprite];
                        break;
                        case "right":
                            CurrentSprite = SpriteImages[7, Sprite];
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
            Bitmap down3 = Properties.Resources.front3;
            Bitmap down4 = Properties.Resources.front4;
            Bitmap left1 = Properties.Resources.left1;
            Bitmap left2 = Properties.Resources.left2;
            Bitmap left3 = Properties.Resources.left3;
            Bitmap left4 = Properties.Resources.left4;
            Bitmap right1 = Properties.Resources.right1;
            Bitmap right2 = Properties.Resources.right2;
            Bitmap right3 = Properties.Resources.right3;
            Bitmap right4 = Properties.Resources.right4;
            //Attacking:
            Bitmap atk_up1 = Properties.Resources.back1;
            Bitmap atk_up2 = Properties.Resources.back2;
            Bitmap atk_up3 = Properties.Resources.back3;
            Bitmap atk_up4 = Properties.Resources.back4;
            Bitmap atk_down1 = Properties.Resources.atk_down1;
            Bitmap atk_down2 = Properties.Resources.atk_down2;
            Bitmap atk_down3 = Properties.Resources.atk_down3;
            Bitmap atk_down4 = Properties.Resources.atk_down4;
            Bitmap atk_left1 = Properties.Resources.left1;
            Bitmap atk_left2 = Properties.Resources.left2;
            Bitmap atk_left3 = Properties.Resources.left3;
            Bitmap atk_left4 = Properties.Resources.left4;
            Bitmap atk_right1 = Properties.Resources.right1;
            Bitmap atk_right2 = Properties.Resources.right2;
            Bitmap atk_right3 = Properties.Resources.right3;
            Bitmap atk_right4 = Properties.Resources.right4;

            // A 2D array, each level corresponding to a direction and their respective images:
            // [up[], down[], left[], right[]]
            return new Bitmap[,] {
                { up1, up2, up3, up4 },
                { down1, down2, down3, down4 },
                { left1, left2, left3, left4 },
                { right1, right2, right3, right4 },
                { atk_up1, atk_up2, atk_up3, atk_up4 },
                { atk_down1, atk_down2, atk_down3, atk_down4 },
                { atk_left1, atk_left2, atk_left3, atk_left4 },
                { atk_right1, atk_right2, atk_right3, atk_right4 }
            };
        }

        public bool IsColliding(Entity e)
        {
            return  BoundingBox.Left <= e.BoundingBox.Right &&
                    BoundingBox.Right >= e.BoundingBox.Left &&
                    BoundingBox.Top <= e.BoundingBox.Bottom &&
                    BoundingBox.Bottom >= e.BoundingBox.Top;
        }

        public void RestrictMovement(Entity otherEntity)
        {
            // Check if there is collision to Top, checking the inner top-half of bounding box.
            if (BoundingBox.Top + BoundingBox.Height/2 >= otherEntity.BoundingBox.Bottom)
            {
                if (BoundingBox.Left + BoundingBox.Width / 2 <= otherEntity.BoundingBox.Right &&
                    BoundingBox.Right - BoundingBox.Width / 2 >= otherEntity.BoundingBox.Left)
                    CanMoveUp = false;
                else
                    CanMoveUp = true;
            }
            // Check if there is collision to the Right, checking the inner half-right of bounding box.
            if (BoundingBox.Right - BoundingBox.Width/2 <= otherEntity.BoundingBox.Left)
            {
                if (BoundingBox.Top + BoundingBox.Height / 2 <= otherEntity.BoundingBox.Bottom &&
                    BoundingBox.Bottom - BoundingBox.Height / 2 >= otherEntity.BoundingBox.Top)
                    CanMoveRight = false;
                else
                    CanMoveRight = true;
            }
            // Check if there is collision Down, checking the inner bottom-half of bounding box.
            if (BoundingBox.Bottom - BoundingBox.Height / 2 >= otherEntity.BoundingBox.Top)
            {
                if (BoundingBox.Left + BoundingBox.Width / 2 <= otherEntity.BoundingBox.Right &&
                    BoundingBox.Right - BoundingBox.Width / 2 >= otherEntity.BoundingBox.Left)
                    CanMoveDown = false;
                else
                    CanMoveDown = true;
            }
            // Check if there is collision to the left
            if (BoundingBox.Left + BoundingBox.Width / 2 >= otherEntity.BoundingBox.Right)
            {
                if (BoundingBox.Top + BoundingBox.Height / 2 <= otherEntity.BoundingBox.Bottom &&
                    BoundingBox.Bottom - BoundingBox.Height / 2 >= otherEntity.BoundingBox.Top)
                    CanMoveLeft = false;
                else
                    CanMoveLeft = true;
            }
        }

        // LevelUp
        private void LevelUp()
        {
            Level++;
            ExperiencePoints = ExperiencePoints - ExpToNextLevel;
            ExpToNextLevel = (int)(ExpToNextLevel * 1.2);

            MaxHealthPoints += 2;
            CurrentHealthPoints = MaxHealthPoints;
            Attack += 2;
            Defense += 1;
            Speed += 1;
        }

        // Gain exp
        public void GainExperience(int experience)
        {
            ExperiencePoints += experience;

            if (ExperiencePoints >= ExpToNextLevel)
            {
                LevelUp();
            }
        }

        public void PlayerAttack()
        {
            if (attackFrame == 0)
            {
                Sprite = 0;
                DisableMovement();
                pSoundEffect.PlayHeroAttackSoundEffect(pSoundEffect.AudioHeroAttack);
                Attack a = new Attack();

                if (Monster.SpawnedMonsters.Count > 0)
                foreach (Monster m in Monster.SpawnedMonsters)
                {
                if (a.BoundingBox.IntersectsWith(m.BoundingBox))
                    {
                        int attackDamage = Math.Max(Attack - m.Defense, 1);
                        int[] arrayToAdd = { attackDamage, 0 };
                        m.damageNumbers.Add(arrayToAdd);
                        m.CurrentHealthPoints -= attackDamage;

                        if (m.CurrentHealthPoints <= 0)
                            m.Die();
                    }
                }
            }
            if (attackFrame == -1)
            {
                IsAttacking = false;
                AllowMovement();
            }
        }
        // add method interact

    }
}

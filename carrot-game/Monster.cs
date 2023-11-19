using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carrot_game
{
    /// <summary>
    /// A monster is a computer-controlled entity that damages the player when they collide.
    /// </summary>
    class Monster : Entity, ICollision
    {
        internal static int Counter = 0;
        public static List<Monster> SpawnedMonsters = new List<Monster>();
        public static List<int[]> damageNumbers = new List<int[]>();
        public string ImgPack { get; set; } = "";
        public Bitmap CurrentSprite;
        private int _attackFrame = 0;
        // seconds between attacks
        private double _attackSpeed = 2;
        // dictionary that will store "damage" as key and how many times it has been displayed as value

        public override Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(PosX, PosY, Width, Height);
            }
        }
        // Controls wether or not action keys are pressed.
        public bool UpPressed, DownPressed, LeftPressed, RightPressed;

        // Move the character on X, Y and Z axis.
        public override void Move(int x, int y, int z)
        {
            PosX += x;
            PosY += y;
            PosZ += z;
        }

        public void FollowPlayer(ref Player p)
        {
            // If player is to the left of the monster 
            if (BoundingBox.Left > p.BoundingBox.Right)
            {
                    RightPressed = false;
                    LeftPressed = true;
            }
            if(BoundingBox.Left <= p.BoundingBox.Right) 
            {
                LeftPressed = false;
            }
            // If player is to the right of the monster
            if (BoundingBox.Right < p.BoundingBox.Left)
            {
                    RightPressed = true;
                    LeftPressed = false;
            }
            if (BoundingBox.Right >= p.BoundingBox.Left)
            {
                RightPressed = false;
            }

            // If player is to the top of the monster
            if (BoundingBox.Top > p.BoundingBox.Bottom)
            {
                    DownPressed = false;
                    UpPressed = true;
            }
            if (BoundingBox.Top <= p.BoundingBox.Bottom)
            {
                UpPressed = false;
            }

            // If player is to the bottom of the monster
            if (BoundingBox.Bottom < p.BoundingBox.Top)
            {
                    DownPressed = true;
                    UpPressed = false;
            }
            if (BoundingBox.Bottom >= p.BoundingBox.Top)
            {
                DownPressed = false;
            }
            
        }

        public void ResolveAttack()
        {
            if (Player.currentPlayer.IsColliding(this))
            {
                _attackFrame++;
                if (_attackFrame > GameScreen.fps*_attackSpeed)
                {
                    int attackDamage = Math.Max(this.Attack - Player.currentPlayer.Defense, 1);
                    int[] arrayToAdd = { attackDamage, 0 };
                    damageNumbers.Add(arrayToAdd);
                    Player.currentPlayer.CurrentHealthPoints -= attackDamage;
                    _attackFrame = 0;
                }
            }
        }
        public void Update()
        {
            ResolveAttack();
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

        public bool IsColliding(Entity player)
        {
            return  PosX < player.PosX + player.Width   &&
                    PosX + Width > player.PosX          &&
                    PosY < player.PosY + player.Height  &&
                    PosY + Height > player.PosY;
        }

        public void Die()
        {
            SpawnedMonsters.Remove(this);
        }

    }
    
}

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace carrot_game
{
    internal class Attack : Entity
    {
        readonly Player p = GameScreen.gs.player;
        public int extraRange = 0;

        public static List<Attack> AttacksList = new List<Attack>();

        internal Attack()
        {
            AttacksList.Add(this);
                Task.Run(() =>
                {
                    Thread.Sleep(200);
                    AttacksList.Remove(this);
                });
        }

        public override Rectangle BoundingBox 
        {
            get
            {
                int verticalSize;
                int horizontalSize;

                switch (p.Direction)
                {
                    case "up":
                        verticalSize = 2 * p.Height / 3 + extraRange * 16; 
                        horizontalSize = p.Width;
                        return new Rectangle(p.BoundingBox.Left - horizontalSize / 3, p.BoundingBox.Top - verticalSize, horizontalSize, verticalSize);
                    case "down":
                        verticalSize = 2 * p.Height / 3 + extraRange * 16;
                        horizontalSize = p.Width;
                        return new Rectangle(p.BoundingBox.Left - horizontalSize / 3, p.BoundingBox.Bottom, horizontalSize, verticalSize);
                    case "left":
                        verticalSize = p.Height;
                        horizontalSize = 2 * p.Width / 3 + extraRange * 16;
                        return new Rectangle(p.BoundingBox.Left - horizontalSize, p.BoundingBox.Top - (verticalSize - p.BoundingBox.Height)/2 , horizontalSize, verticalSize);
                    case "right":
                        verticalSize = p.Height;
                        horizontalSize = 2 * p.Width / 3 + extraRange * 16;
                        return new Rectangle(p.BoundingBox.Right, p.BoundingBox.Top - (verticalSize - p.BoundingBox.Height) / 2, horizontalSize, verticalSize);
                    default:
                        return new Rectangle();
                }
            }
        }

        public override void Move(int x, int y, int z)
        {
            // Implement projectile attacks here
        }
    }
}
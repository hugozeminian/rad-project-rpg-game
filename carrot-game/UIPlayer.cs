using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace carrot_game
{
    internal class UIPlayer : Control
    {
        private int _maximum = 100;
        private int _minimum = 0;
        private int _value = 50;
        private int _linearProgressBarHeight = 30;

        // Properties Carrot
        public Image carrot = Properties.Resources.carrot;
        public int carrotAmount { get; set; } = 0;

        // Properties LVL
        public int playerLevel { get; set; } = 1;

        // Properties for circular progress bar (XP)
        public Point CircularProgressBarLocation { get; set; } = new Point(20, 1220);
        public Size CircularProgressBarSize { get; set; } = new Size(200, 200);
        public int xpCurrentValue { get; set; } = 0;

        // Properties for linear progress bar (HP)
        public Point LinearProgressBarLocation { get; set; } = new Point(20, 40);
        public Size LinearProgressBarSize { get; set; } = new Size(300, 30);
        public int hpCurrentValue { get; set; } = 100;

        public UIPlayer()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawCircularProgressBar(e.Graphics);
            DrawLinearProgressBar(e.Graphics);
            DrawCarrot(e.Graphics);
        }

        public void DrawCircularProgressBar(Graphics g)
        {
            Brush backgroundBrush = new SolidBrush(Color.Gray);
            Brush fillBrush = new SolidBrush(Color.Green);

            float angle = 360f * (xpCurrentValue - _minimum) / (_maximum - _minimum);

            // CircularProgressBarLocation and CircularProgressBarSize properties
            g.FillEllipse(backgroundBrush, CircularProgressBarLocation.X, CircularProgressBarLocation.Y, CircularProgressBarSize.Width, CircularProgressBarSize.Height);
            g.FillPie(fillBrush, CircularProgressBarLocation.X, CircularProgressBarLocation.Y, CircularProgressBarSize.Width, CircularProgressBarSize.Height, -90, angle);

            // Draw the level label
            string levelText = "LVL " + playerLevel.ToString();
            Font levelFont = new Font("Arial", 24, FontStyle.Bold);
            Brush levelBrush = new SolidBrush(Color.White);
            SizeF levelSize = g.MeasureString(levelText, levelFont);
            PointF levelLocation = new PointF(CircularProgressBarLocation.X + (CircularProgressBarSize.Width - levelSize.Width) / 2, CircularProgressBarLocation.Y + (CircularProgressBarSize.Height - levelSize.Height) / 2);
            g.DrawString(levelText, levelFont, levelBrush, levelLocation);
        }

        public void DrawLinearProgressBar(Graphics g)
        {
            Brush backgroundBrush = new SolidBrush(Color.Gray);
            Brush fillBrush = new SolidBrush(Color.Blue);

            int fillWidth = (int)((float)(hpCurrentValue - _minimum) / (_maximum - _minimum) * LinearProgressBarSize.Width);

            // LinearProgressBarLocation and LinearProgressBarSize properties
            g.FillRectangle(backgroundBrush, LinearProgressBarLocation.X, LinearProgressBarLocation.Y, LinearProgressBarSize.Width, LinearProgressBarSize.Height);
            g.FillRectangle(fillBrush, LinearProgressBarLocation.X, LinearProgressBarLocation.Y + LinearProgressBarSize.Height - _linearProgressBarHeight, fillWidth, _linearProgressBarHeight);
        }

        public void DrawCarrot(Graphics g)
        {
            // Calculate carrot position based on LinearProgressBarLocation and LinearProgressBarSize
            int carrotX = LinearProgressBarLocation.X + 320;
            int carrotY = LinearProgressBarLocation.Y + (LinearProgressBarSize.Height - 80) / 2;

            int carrotWidth = 80;
            int carrotHeight = 80;

            g.DrawImage(carrot, new Rectangle(carrotX, carrotY, carrotWidth, carrotHeight));

            // Draw the carrot label
            string label = "x " + carrotAmount.ToString();
            Font labelFont = new Font("Arial", 14, FontStyle.Bold);
            Brush labelBrush = new SolidBrush(Color.Black);
            SizeF labelSize = g.MeasureString(label, labelFont);

            // Adjust the label location
            PointF labelLocation = new PointF(carrotX + carrotWidth - 10, carrotY + (carrotHeight - labelSize.Height) / 2);

            g.DrawString(label, labelFont, labelBrush, labelLocation);
        }



    }
}

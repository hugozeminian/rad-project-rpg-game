using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace carrot_game
{
    /// <summary>
    /// UI Player - 
    /// </summary>
    internal class UIPlayer : Control
    {
        private int _maximum = 100;
        private int _minimum = 0;
        private int _linearProgressBarHeight = 30;

        Screen screen;
        Rectangle workingArea;

        // Properties Carrot
        public Image carrot = Properties.Resources.carrot;


        // Properties for circular progress bar (XP)
        //public Point CircularProgressBarLocation { get; set; } = new Point(20, 1220);
        public Point CircularProgressBarLocation { get; set; }
        public Size CircularProgressBarSize { get; set; }
        public int xpCurrentValue { get; set; } = 0;


        // Properties for linear progress bar (HP)
        public Point LinearProgressBarLocation { get; set; }
        public Size LinearProgressBarSize { get; set; }
        public int hpCurrentValue { get; set; } = 100;

        public UIPlayer()
        {
            screen = Screen.FromControl(this);
            workingArea = screen.WorkingArea;

            //CircularProgressBarLocation = new Point(workingArea.X + (workingArea.Width) / 2 - Size.Width / 2, workingArea.Y + workingArea.Height - 160);
            CircularProgressBarLocation = new Point(workingArea.X + 40, workingArea.Y + workingArea.Height - CircularProgressBarSize.Height - 190);

            CircularProgressBarSize = new Size(200, 200);


            LinearProgressBarLocation = new Point(workingArea.X + 40, workingArea.Y + 40);
            LinearProgressBarSize = new Size(300, 30);
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

            float angle = 360f * (Player.currentPlayer.ExperiencePoints) / (Player.currentPlayer.ExpToNextLevel);

            // CircularProgressBarLocation and CircularProgressBarSize properties
            g.FillEllipse(backgroundBrush, CircularProgressBarLocation.X, CircularProgressBarLocation.Y, CircularProgressBarSize.Width, CircularProgressBarSize.Height);
            g.FillPie(fillBrush, CircularProgressBarLocation.X, CircularProgressBarLocation.Y, CircularProgressBarSize.Width, CircularProgressBarSize.Height, -90, angle);

            // Draw the level label
            string levelText = "LVL " + Player.currentPlayer.Level.ToString();
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

            int fillWidth = (int)((float)(Player.currentPlayer.CurrentHealthPoints) / (Player.currentPlayer.MaxHealthPoints) * LinearProgressBarSize.Width);

            // Draw the HP label
            Font labelFont = new Font("Arial", 18, FontStyle.Bold); 
            Brush labelBrush = new SolidBrush(Color.Black);
            g.DrawString("HP:", labelFont, labelBrush, LinearProgressBarLocation.X, LinearProgressBarLocation.Y);

            // LinearProgressBarLocation and LinearProgressBarSize properties
            g.FillRectangle(backgroundBrush, LinearProgressBarLocation.X + labelFont.SizeInPoints * 3, LinearProgressBarLocation.Y, LinearProgressBarSize.Width, LinearProgressBarSize.Height);
            g.FillRectangle(fillBrush, LinearProgressBarLocation.X + labelFont.SizeInPoints * 3, LinearProgressBarLocation.Y + LinearProgressBarSize.Height - _linearProgressBarHeight, fillWidth, _linearProgressBarHeight);
        }

        public void DrawCarrot(Graphics g)
        {
            // Calculate carrot position based on LinearProgressBarLocation and LinearProgressBarSize
            int carrotX = LinearProgressBarLocation.X + 380;
            int carrotY = LinearProgressBarLocation.Y + (LinearProgressBarSize.Height - 80) / 2;

            int carrotWidth = 43;
            int carrotHeight = 64;

            g.DrawImage(carrot, new Rectangle(carrotX, carrotY, carrotWidth, carrotHeight));

            // Draw the carrot label
            string label = "x " + Player.currentPlayer.Carrots.ToString();
            Font labelFont = new Font("Arial", 14, FontStyle.Bold);
            Brush labelBrush = new SolidBrush(Color.Black);
            SizeF labelSize = g.MeasureString(label, labelFont);

            // Adjust the label location
            PointF labelLocation = new PointF(carrotX + carrotWidth - 5, carrotY + (carrotHeight - labelSize.Height) / 2 + 5);

            g.DrawString(label, labelFont, labelBrush, labelLocation);
        }



    }
}

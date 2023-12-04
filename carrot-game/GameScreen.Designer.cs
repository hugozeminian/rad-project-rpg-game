using System.Drawing;
using System.Windows.Forms;

namespace carrot_game
{
    partial class GameScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 16;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::carrot_game.Properties.Resources.grass;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.Name = "GameScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ActualGameScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameScreen_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameScreen_FormClosed);
            this.Load += new System.EventHandler(this.GameScreen_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameScreen_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameScreen_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
    }
}
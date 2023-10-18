namespace carrot_game
{
    partial class ActualGameScreen
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
            this.picHero = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picHero)).BeginInit();
            this.SuspendLayout();
            // 
            // picHero
            // 
            this.picHero.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picHero.Image = global::carrot_game.Properties.Resources.front2;
            this.picHero.Location = new System.Drawing.Point(256, 184);
            this.picHero.Name = "picHero";
            this.picHero.Size = new System.Drawing.Size(68, 71);
            this.picHero.TabIndex = 0;
            this.picHero.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer1.Interval = ActualGameScreen.fps;
            // 
            // ActualGameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picHero);
            this.Name = "ActualGameScreen";
            this.Text = "ActualGameScreen";
            ((System.ComponentModel.ISupportInitialize)(this.picHero)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picHero;
        private System.Windows.Forms.Timer timer1;
    }
}
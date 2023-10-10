namespace carrot_game
{
    partial class FormIntro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIntro));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mediaIntro = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.mediaIntro)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 4200;
            // 
            // mediaIntro
            // 
            this.mediaIntro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaIntro.Enabled = true;
            this.mediaIntro.Location = new System.Drawing.Point(0, 0);
            this.mediaIntro.Name = "mediaIntro";
            this.mediaIntro.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mediaIntro.OcxState")));
            this.mediaIntro.Size = new System.Drawing.Size(300, 294);
            this.mediaIntro.TabIndex = 0;
            // 
            // FormIntro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 294);
            this.Controls.Add(this.mediaIntro);
            this.Name = "FormIntro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Carrot-Game";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.formIntro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediaIntro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mediaIntro;
        private System.Windows.Forms.Timer timer;
    }
}


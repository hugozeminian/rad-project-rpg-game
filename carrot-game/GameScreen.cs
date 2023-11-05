using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
=======
using System.Drawing;
>>>>>>> 346314b5003fcd9e818b52dc26f7b0c19889ea15
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace carrot_game
{
    public partial class GameScreen : Form
    {
        Player heroCharacter = new Player();
        Monster m = new WhiteBunny();

<<<<<<< HEAD
        public int savePos;
        public GameScreen gs; 
=======
        private AxWMPLib.AxWindowsMediaPlayer mediaIntro;
        private System.Windows.Forms.Timer timer;

>>>>>>> 346314b5003fcd9e818b52dc26f7b0c19889ea15

        // Declaring a refresh rate of 30 frames per second [33.33ms] (1000ms / 30)
        public static int fps = 30;
        public static int refreshTime = 1000/fps;

        Audio bgm = new Audio();

        public GameScreen()
        {
            InitializeComponent();
<<<<<<< HEAD
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = new Size(1920, 1080);
            gs = this;
            bgm.playAudioBackgroud(bgm.audioBackgroundPhase1);
=======

            // Remove media player controls
            mediaIntro.uiMode = "none";

            //// Load the menu song
            //SoundPlayer bgm = new SoundPlayer("res\\sound\\bgm\\menu.wav");
            //bgm.PlayLooping();

            // Set the form to full screen
            this.WindowState = FormWindowState.Maximized;
>>>>>>> 346314b5003fcd9e818b52dc26f7b0c19889ea15
        }
        public GameScreen(int save) : this() 
        {
<<<<<<< HEAD
            savePos = save;
            heroCharacter = new Player(save);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Refresh redraws everything in the form.
            Refresh();
            // updates the character's position and sprite image.
            heroCharacter.Update();
            if (Monster.Counter == 0)
            m = new WhiteBunny();
            m.FollowPlayer(heroCharacter);
            m.Update();
            
            
        }

        private void InitializeObjects()
        {
        }

        private void PaintObjects(object sender, PaintEventArgs e)
        {
            // Create a Graphics object to draw on the form
            Graphics g = e.Graphics;

            // Draw the image at the character's position (PosX, PosY)
            g.DrawImage(heroCharacter.CurrentSprite, heroCharacter.PosX, heroCharacter.PosY, heroCharacter.Width, heroCharacter.Height); 

            g.DrawImage(m.CurrentSprite, m.PosX, m.PosY, m.Width, m.Height);
=======
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameScreen));
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mediaIntro = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediaIntro)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(120)))), ((int)(((byte)(20)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Ink Free", 48F);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnExit.Location = new System.Drawing.Point(0, 339);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(524, 80);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseEnter += new System.EventHandler(this.btnHover);
            this.btnExit.MouseLeave += new System.EventHandler(this.btnLeaveHover);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.btnNewGame);
            this.panel1.Controls.Add(this.btnContinue);
            this.panel1.Controls.Add(this.btnLoadGame);
            this.panel1.Controls.Add(this.btnOptions);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(170, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(855, 483);
            this.panel1.TabIndex = 2;
            // 
            // btnNewGame
            // 
            this.btnNewGame.FlatAppearance.BorderSize = 0;
            this.btnNewGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(120)))), ((int)(((byte)(20)))));
            this.btnNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewGame.Font = new System.Drawing.Font("Ink Free", 48F);
            this.btnNewGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnNewGame.Location = new System.Drawing.Point(0, 53);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(524, 80);
            this.btnNewGame.TabIndex = 5;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.MouseEnter += new System.EventHandler(this.btnHover);
            this.btnNewGame.MouseLeave += new System.EventHandler(this.btnLeaveHover);
            // 
            // btnContinue
            // 
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(120)))), ((int)(((byte)(20)))));
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Ink Free", 48F);
            this.btnContinue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnContinue.Location = new System.Drawing.Point(0, 124);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(524, 80);
            this.btnContinue.TabIndex = 4;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.MouseEnter += new System.EventHandler(this.btnHover);
            this.btnContinue.MouseLeave += new System.EventHandler(this.btnLeaveHover);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.FlatAppearance.BorderSize = 0;
            this.btnLoadGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(120)))), ((int)(((byte)(20)))));
            this.btnLoadGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadGame.Font = new System.Drawing.Font("Ink Free", 48F);
            this.btnLoadGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnLoadGame.Location = new System.Drawing.Point(0, 193);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(524, 80);
            this.btnLoadGame.TabIndex = 3;
            this.btnLoadGame.Text = "Load Game";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            this.btnLoadGame.MouseEnter += new System.EventHandler(this.btnHover);
            this.btnLoadGame.MouseLeave += new System.EventHandler(this.btnLeaveHover);
            // 
            // btnOptions
            // 
            this.btnOptions.FlatAppearance.BorderSize = 0;
            this.btnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(120)))), ((int)(((byte)(20)))));
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptions.Font = new System.Drawing.Font("Ink Free", 48F);
            this.btnOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnOptions.Location = new System.Drawing.Point(0, 260);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(524, 80);
            this.btnOptions.TabIndex = 2;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            this.btnOptions.MouseEnter += new System.EventHandler(this.btnHover);
            this.btnOptions.MouseLeave += new System.EventHandler(this.btnLeaveHover);
            // 
            // timer
            // 
            this.timer.Interval = 4200;
            // 
            // mediaIntro
            // 
            this.mediaIntro.Enabled = true;
            this.mediaIntro.Location = new System.Drawing.Point(0, 0);
            this.mediaIntro.Name = "mediaIntro";
            this.mediaIntro.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mediaIntro.OcxState")));
            this.mediaIntro.Size = new System.Drawing.Size(498, 266);
            this.mediaIntro.TabIndex = 3;
            // 
            // GameScreen
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BackgroundImage = global::carrot_game.Properties.Resources.Main;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(852, 480);
            this.Controls.Add(this.mediaIntro);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Carrot-Game";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.GameScreen_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediaIntro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

>>>>>>> 346314b5003fcd9e818b52dc26f7b0c19889ea15
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            InitializeObjects();

            // Add a Paint event handler
            this.Paint += new PaintEventHandler(this.PaintObjects);

            // Start the timer for redrawing
            timer1.Start();
=======
            this.ClientSize = new System.Drawing.Size(1920, 1080);

            //### PLAYER ###
            // Assign our media player url to display our intro video
            mediaIntro.URL = "res\\video\\IntroCarrot.mp4";
            mediaIntro.Dock = DockStyle.Fill;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            mediaIntro.Ctlcontrols.play();

            // Set this form to fullscreen 1920x1080 -
            // ToDO - Add exception handling if the display doesn't support chosen resolution.
            // ToDo - Save resolution to a file and read it whenever we open the game
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Size = new Size(1920, 1080);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            mediaIntro.Enabled = false;
            mediaIntro.Visible = false;
>>>>>>> 346314b5003fcd9e818b52dc26f7b0c19889ea15
        }

        private void GameScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToFile(savePos);
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            KeyHandler.HandleKeyDown(e, heroCharacter, gs);
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            KeyHandler.HandleKeyRelease(e, heroCharacter, gs);
        }

        private void SaveToFile(int SavePosition)
        {
            string path = $"save{SavePosition}.txt";
            // This should run only the first time the game is closed, if the user hasn't manually saved it.
            File.Delete(path);
                using (FileStream fs = File.Create(path))
                {
                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(heroCharacter))
                    {
                    string name = prop.Name;
                    if (prop.PropertyType == typeof(string))
                            AddText(fs, $"{name}=\"{prop.GetValue(heroCharacter)}\"\n");
                    else    AddText(fs, $"{name}={prop.GetValue(heroCharacter)}\n");
                    }
                    fs.Seek(-2, SeekOrigin.End);
                    fs.Close();
                }
        }
        // Encoding function Obtained from Microsoft Learning: (https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-7.0)
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void GameScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

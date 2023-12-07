using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carrot_game
{
    partial class MainMenu : Form
    {
        // Change this to skip intro video.
        internal static bool skipIntro = false;

        public static MainMenu instance;
        public static bool bgmOn = true;

        private static Color _menuGreen = Color.FromArgb(255, 0, 192, 0);
        private void MainMenu_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(1920, 1080);

            //### PLAYER ###
            // Assign our media player url to display our intro video
            if (!skipIntro)
            {
                mediaIntro.Visible = true;
                mediaIntro.settings.volume = 0;
                mediaIntro.URL = "res\\video\\IntroCarrot.mp4";
                mediaIntro.Dock = DockStyle.Fill;
                mediaIntro.uiMode = "none";

                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
                mediaIntro.Ctlcontrols.play();
            }

            // Set this form to fullscreen 1920x1080 -
            // ToDO - Add exception handling if the display doesn't support chosen resolution.
            // ToDo - Save resolution to a file and read it whenever we open the game
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = new Size(1920, 1080);
            SetOptions();
            instance = this;
            Program.CurrentScreen = "Main Menu";
        }

        private void SetOptions()
        {
            if (bgmOn)
            {
                btnBgm.Text = "ON";
                Options.bgm = true;
            }
            else
            {
                btnBgm.Text = "OFF";
                Options.bgm = false;
            }
            btnBoundingBoxes.Text = Options.showBoundingBox ? "ON" : "OFF";
            btnPlayerName.Text = Options.showPlayerName ? "ON" : "OFF";
            btnMonsterNames.Text = Options.showMonsterNames ? "ON" : "OFF";

            btnMonsterNames.BackColor = btnMonsterNames.Text == "ON" ? _menuGreen : Color.Red;
            btnBoundingBoxes.BackColor = btnBoundingBoxes.Text == "ON" ? _menuGreen : Color.Red;
            btnBgm.BackColor = btnBgm.Text == "ON" ? _menuGreen : Color.Red;
            btnBoundingBoxes.BackColor = btnBoundingBoxes.Text == "ON" ? _menuGreen : Color.Red;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            mediaIntro.Enabled = false;
            mediaIntro.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            pnlList.Visible = false;
            pnlOptions.Visible = true;
        }

        private void btnHover (object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.ForeColor = Color.Black;
        }

        private void btnLeaveHover(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.ForeColor = Color.FromArgb(40,40,40);
        }

        internal void btnNewGame_Click(object sender, EventArgs e)
        {
            pnlList.Visible = false;
            NameInputDialog1.Visible = true;
            NameInputDialog1.Dock = DockStyle.Fill;
            NameInputDialog1.nameInput.Focus();

            GameScreen.newGame = true;
            bgm.StopAudioBackgroud();
            bgm.Dispose();


            NameInputDialog1.Focus();    
        }


        private void btnContinue_Click(object sender, EventArgs e)
        {
            GameScreen.newGame = false;
            bgm.StopAudioBackgroud();
            bgm.Dispose();
            Form game = new GameScreen(0);
            game.Show();
            Close();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            btnContinue_Click(sender, e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlOptions.Visible = false;
            pnlList.Visible = true;
        }


        private void btnBgm_Click(object sender, EventArgs e)
        {
            if (btnBgm.Text == "ON")
            {
                btnBgm.Text = "OFF";
                Options.bgm = false;
                btnBgm.BackColor = Color.Red;
                bgm.StopAudioBackgroud();
            }
            else if (btnBgm.Text == "OFF")
            {
                btnBgm.Text = "ON";
                Options.bgm = true;

                btnBgm.BackColor = Color.FromArgb(255,0,192,0);
                bgm.PlayAudioBackgroud(bgm.AudioMenu);
            }
        }

        private void btnMonsterNames_Click(object sender, EventArgs e)
        {
            if (btnMonsterNames.Text == "ON")
            {
                btnMonsterNames.Text = "OFF";
                btnMonsterNames.BackColor = Color.Red;
                GameScreen.showMonsterNames = false;
            }
            else if (btnMonsterNames.Text == "OFF")
            {
                btnMonsterNames.Text = "ON";
                btnMonsterNames.BackColor = Color.FromArgb(255, 0, 192, 0);
                GameScreen.showMonsterNames = true;
            }
        }

        private void btnPlayerName_Click(object sender, EventArgs e)
        {
            if (btnPlayerName.Text == "ON")
            {
                btnPlayerName.Text = "OFF";
                btnPlayerName.BackColor = Color.Red;
                GameScreen.showPlayerName = false;
            }
            else if (btnPlayerName.Text == "OFF")
            {
                btnPlayerName.Text = "ON";
                btnPlayerName.BackColor = Color.FromArgb(255, 0, 192, 0);
                GameScreen.showPlayerName = true;
            }
        }

        private void btnBoundingBoxes_Click(object sender, EventArgs e)
        {
            if (btnBoundingBoxes.Text == "ON")
            {
                btnBoundingBoxes.Text = "OFF";
                btnBoundingBoxes.BackColor = Color.Red;
                Options.showBoundingBox = false;
            }
            else if (btnBoundingBoxes.Text == "OFF")
            {
                btnBoundingBoxes.Text = "ON";
                btnBoundingBoxes.BackColor = Color.FromArgb(255, 0, 192, 0);
                Options.showBoundingBox = true;
            }
        }
    }
}

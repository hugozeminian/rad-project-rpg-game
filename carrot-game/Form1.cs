using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

using WMPLib;

namespace carrot_game
{
    /// <summary>
    /// FormIntro is the Windows Form that displays our group's introduction video. It should be set to fullscreen. When closed, the MainMenu should open.
    /// </summary>
    public partial class FormIntro : Form
    {
        public FormIntro()
        {
            InitializeComponent();
            mediaIntro.uiMode = "none";
        }

        private void formIntro_Load(object sender, EventArgs e)
        {
            // Assign our media player url to display our intro video
            mediaIntro.URL = "res\\video\\IntroCarrot.mp4";

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
            this.Close();
        }
    }
}

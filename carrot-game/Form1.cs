using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carrot_game
{
    public partial class formIntro : Form
    {
        public formIntro()
        {
            InitializeComponent();
        }

        private void formIntro_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width >= 1919 && Screen.PrimaryScreen.Bounds.Height >= 1079)
            {
                // set this form to 1920x1080 resolution
                this.Height = 1080;
                this.Width = 1920;
            }
            else
            {
                this.Height = 768;
                this.Width = 1280;
            }

                // Set this form to fullscreen.
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

            // Display a message box with the current resolution
            MessageBox.Show($"Resolution: \n{Width} : {Height}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

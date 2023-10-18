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
    public partial class ActualGameScreen : Form
    {
        // Declaring a refresh rate of 60 frames per second (1000ms / 60)
        public static int fps = 1000/60;
        public ActualGameScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // this redraws everything in the form
            this.Refresh();
        }
    }
}

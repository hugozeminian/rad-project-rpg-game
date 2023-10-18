using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carrot_game
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormIntro());
            Application.Run(new GameScreen());

            Player mainCharacter = new Player();
            // initialize a KeyHandler
            // initialize GameScreen
            // Create a player character
        }
    }
}

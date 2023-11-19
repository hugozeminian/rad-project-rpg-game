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
        internal static string CurrentScreen = "Main Menu";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var main = new MainMenu();
            main.Show();
            Application.Run();

            // initialize a KeyHandler
            // initialize GameScreen
            // Create a player character
        }

        public static void FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) Application.ExitThread();
        }
    }
}

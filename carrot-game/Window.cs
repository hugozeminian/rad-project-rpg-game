using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carrot_game
{
    /// <summary>
    /// A window that displays content in the player's screen. 
    /// </summary>
    // Should the screen have a fixed size? Should it be set to fullscreen? Can the user change these settings in the options menu?
    // Other screens will inherit properties from this class.
    internal class Window

    {
        internal int Height { get; set; }
        internal int Width { get; set; }

    public Window() {
        // Get the primary window's dimensions
        this.Height = Screen.PrimaryScreen.Bounds.Height;
        this.Width = Screen.PrimaryScreen.Bounds.Width;

        }
        // Obtain the player's window size and make the game's window match that.
        // Set a minimum size of 1024x768 pixels
        // Center the display.
        // Allow the changes to be saved (tip: create a text file that saves this information)
        // Read the saved configuration files when initializing the game.


    }
}

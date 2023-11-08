using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace carrot_game
{
    public partial class GameScreen : Form
    {
        // Define constants for the screen width and height
        private const int ScreenWidth = 1920;
        private const int ScreenHeight = 1080;

        private Map gameMap;

        Player heroCharacter = new Player();
        Monster m = new WhiteBunny();

        public int savePos;
        public GameScreen gs; 

        // Declaring a refresh rate of 30 frames per second [33.33ms] (1000ms / 30)
        public static int fps = 30;
        public static int refreshTime = 1000/fps;

        Audio bgm = new Audio();

        public GameScreen()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = new Size(ScreenWidth, ScreenHeight);
            gs = this;

            bgm.playAudioBackgroud(bgm.audioBackgroundPhase1);

            CreateMap();
            Program.CurrentScreen = "Game Screen";
        }
        public GameScreen(int save) : this() 
        {
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

        private void PaintObjects(object sender, PaintEventArgs e)
        {
            // Create a Graphics object to draw on the form
            Graphics g = e.Graphics;

            // Draw the image at the character's position (PosX, PosY)
            g.DrawImage(heroCharacter.CurrentSprite, heroCharacter.PosX, heroCharacter.PosY, heroCharacter.Width, heroCharacter.Height); 

            g.DrawImage(m.CurrentSprite, m.PosX, m.PosY, m.Width, m.Height);
        }

        private void PaintMap(object sender, PaintEventArgs e)
        {
            gameMap.Draw(e.Graphics);
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            // Add a Paint event handler
            this.Paint += new PaintEventHandler(this.PaintMap);
            this.Paint += new PaintEventHandler(this.PaintObjects);

            // Start the timer for redrawing
            timer1.Start();
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
            bgm.stopAudioBackgroud();
            MainMenu m = new MainMenu();
            m.Show();
        }

        private void CreateMap()
        {

            //Map data as a 2D array [22 rows and 40 col]
            int[,] mapData = new int[,]
            {
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19, 19, 19, 19},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19,  0,  0, 19},
                {0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19,  0,  0, 19},
                {0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19, 19, 19, 19},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18, 18,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 18, 18,  0, 12,  2,  5, 14,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0, 12,  4,  3, 14, 12,  5,  6, 16,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0},
                {0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 12,  2,  3, 16, 15,  4,  2,  4, 14,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 12,  5,  4,  5,  6,  3,  4,  3, 14,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  7,  2,  3,  2,  2,  5,  2, 16,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  7,  4,  2,  4,  2,  3,  5, 14,  0,  0,  0,  1,  0,  0,  0,  0},
                {0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 12,  2,  6,  3,  3,  4,  6, 14,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  7,  2,  9,  3,  2,  8,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 10,  0, 10, 10,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  1,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}
            };

            // Create the map
            gameMap = new Map(mapData);
        }
    }
}

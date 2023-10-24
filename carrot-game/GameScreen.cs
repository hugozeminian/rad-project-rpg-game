﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public int savePos;
        public GameScreen gs; 

        // Declaring a refresh rate of 30 frames per second [33.33ms] (1000ms / 30)
        public static int fps = 30;
        public static int refreshTime = 1000/fps;

        public GameScreen()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = new Size(1920, 1080);
            gs = this;
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
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            InitializeObjects();

            // Add a Paint event handler
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
            Application.Exit();
        }
    }
}

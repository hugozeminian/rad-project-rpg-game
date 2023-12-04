using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace carrot_game
{
    public partial class GameScreen : Form
    {
        // Define constants for the screen width and height
        private const int ScreenWidth = 1920;
        private const int ScreenHeight = 1080;

        // This sets how much bigger entities are drawn on the GameScreen.
        public static int GlobalScale = 2;

        // Limits the number of monsters
        private static int _monsterLimit = 1;

        // List of items carrots
        private List<Item> carrots = new List<Item>();
        private static int _carrotsLimit = 3;

        internal Map gameMap = new Map();

        UIPlayer uIPlayer = new UIPlayer();

        internal Player player = new Player();
        internal static Player P
        {
            get
            {
                return Player.currentPlayer;
            }
        }

        public int savePos;
        public static GameScreen gs; 

        // Declaring a refresh rate of 30 frames per second [33.33ms] (1000ms / 30)
        public static int fps = 30;
        public static int refreshTime = 1000/fps;
        public static int monsterSpawnTimer = 0;

        Audio bgm = new Audio();

        // Declaring the name tag variables:
        public static bool showPlayerName = true;
        public static bool showMonsterNames = true;

        private static readonly StringFormat sf = new StringFormat();
        private readonly Font PlayerNameTag = new Font("Georgia", 7 * GlobalScale, FontStyle.Bold, GraphicsUnit.Point);
        private readonly Font MonsterNameTag = new Font("Georgia", 6 * GlobalScale, GraphicsUnit.Point);

        public GameScreen()
        {
            InitializeComponent();

            CenterScreen(player);

            // Format window:
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = new Size(ScreenWidth, ScreenHeight);
            gs = this;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            // Manage options choice
            if (Options.bgm)
                bgm.PlayAudioBackgroud(bgm.AudioBackgroundPhase1);
            Program.CurrentScreen = "Game Screen";
        }
        public GameScreen(int save) : this() 
        {
            savePos = save;
            player = new Player(save);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Refresh redraws everything in the form.
            Refresh();

            // Deal with collisions
            player.AllowMovement();
            foreach (var m in Monster.SpawnedMonsters)
            {
                if (player.IsColliding(m))
                    player.RestrictMovement(m);
            }

            // updates the character's position and sprite image.
            Task updatePlayer = Task.Run(() => player.Update());
            Task spawnMonsters = Task.Run(() => SpawnMonsters());
            Task animateMonsters = Task.Run(() => AnimateMonsters());
            Task updateMonsters = Task.Run(() => UpdateMonsters(Monster.SpawnedMonsters));

            // Spawn a new carrot every x seconds
            monsterSpawnTimer++;
            if (monsterSpawnTimer % (fps * 10) == 0 )
            {
                Task spawnCarrot = Task.Run(() => SpawnCarrot());
            }

            // Check and update carrot collection
            CollectCarrots();
        }

        private void AnimateMonsters()
        {
            foreach (Monster _m in  Monster.SpawnedMonsters)
            {
                _m.FollowPlayer(player);
            }
        }

        // Solution to center player based on https://stackoverflow.com/a/18496302
        internal void CenterScreen(Player p)
        {
            Screen screen = Screen.FromControl(this);
            Rectangle workingArea = screen.WorkingArea;
            p.ScreenX = workingArea.X + (workingArea.Width - p.Width) / 2 - MapTile.tileSize / 2;
            p.ScreenY = workingArea.Y + (workingArea.Height - p.Height) / 2 - MapTile.tileSize / 2;
        }

        private void PaintObjects(object sender, PaintEventArgs e)
        {
            Rectangle _pr = new Rectangle(player.BoundingBox.Location.X - 5, player.BoundingBox.Location.Y + player.BoundingBox.Height - 20, player.BoundingBox.Width + 10, 25);
            // Create a Graphics object to draw on the form
            Graphics g = e.Graphics;

            // Draw carrots
            foreach (var c in carrots)
            {
                if (!c.IsCollected)
                {
                    g.DrawImage(c.CarrotImage, c.ScreenX, c.ScreenY, c.Width, c.Height);
                }
            }

            // Draw monsters with lower z-index:
            DrawMonsters(g, 0);

            // Draw our Hero's Shadow:
            g.FillEllipse(new SolidBrush(Color.FromArgb(120, 40, 40, 40)), _pr);
            // Draw our Hero:
            g.DrawImage(player.CurrentSprite, player.ScreenX, player.ScreenY, player.Width, player.Height);
            if (Options.showBoundingBox == true)
            {
                g.DrawRectangle(new Pen(Color.Magenta, 3f), P.BoundingBox);
            }
            if (showPlayerName == true)
            {
                Rectangle nameTag = new Rectangle(player.ScreenX, player.ScreenY - 20, player.Width, 20);
                g.DrawString(player.Name, PlayerNameTag, Brushes.Black, nameTag, sf);
            }
            // Draw monsters with higher z-index:
            DrawMonsters(g, 1);

            // Draw damage numbers:
            DrawDamage(g);
        }

        private void PaintMap(object sender, PaintEventArgs e)
        {
            gameMap.Draw(e.Graphics, gs);
        }

        private void PaintUIPlayer(object sender, PaintEventArgs e)
        {
            uIPlayer.DrawCircularProgressBar(e.Graphics);
            uIPlayer.DrawLinearProgressBar(e.Graphics);
            uIPlayer.DrawCarrot(e.Graphics);
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            // Add a Paint event handler
            this.Paint += new PaintEventHandler(this.PaintMap);
            this.Paint += new PaintEventHandler(this.PaintObjects);
            this.Paint += new PaintEventHandler(this.PaintUIPlayer);

            // Start the timer for redrawing
            timer1.Start();
        }

        private void GameScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToFile(savePos);
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            KeyHandler.HandleKeyDown(e, player, gs);
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            KeyHandler.HandleKeyRelease(e, player, gs);
        }

        private void SaveToFile(int SavePosition)
        {
            string path = $"save{SavePosition}.txt";
            // This should run only the first time the game is closed, if the user hasn't manually saved it.
            File.Delete(path);
                using (FileStream fs = File.Create(path))
                {
                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(player))
                    {
                    string name = prop.Name;
                    if (prop.PropertyType == typeof(string))
                            AddText(fs, $"{name}=\"{prop.GetValue(player)}\"\n");
                    else    AddText(fs, $"{name}={prop.GetValue(player)}\n");
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

        private void UpdateMonsters(List<Monster> monsters)
        {
            foreach (Monster m in monsters)
            {
                m.Update();
            }
        }

        private void DrawMonsters(Graphics g, int pos)
        {
            int nameTagTextGap = 5;
            int nameTagHeight = 20;
            int nameTagMaxWidth = 200;

            foreach (Monster m in Monster.SpawnedMonsters)
            {
                m.ScreenX = m.WorldX - gs.player.WorldX - gs.player.ScreenX;
                m.ScreenY = m.WorldY - gs.player.WorldY - gs.player.ScreenY;

                // rectangle that contains the monster shadow
                Rectangle _r = new Rectangle(m.BoundingBox.Location.X - 5, m.BoundingBox.Location.Y + m.BoundingBox.Height - 20, m.BoundingBox.Width + 10, 25);

                // pos == 1 means draw on top of player
                if (m.BoundingBox.Bottom >= player.BoundingBox.Bottom - player.BoundingBox.Height/2 && pos == 1) 
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(190, 40, 40, 40)), _r);
                    g.DrawImage(m.CurrentSprite, m.ScreenX, m.ScreenY, m.Width, m.Height);
                    if (Options.showBoundingBox == true)
                    {
                        g.DrawRectangle(Pens.Red, m.BoundingBox);
                    }
                    if (showMonsterNames == true)
                    {
                        Rectangle nameTag = new Rectangle(m.ScreenX - (nameTagMaxWidth - m.Width)/2, m.ScreenY - nameTagHeight - nameTagTextGap, nameTagMaxWidth, nameTagHeight);
                        g.DrawString(m.Name, MonsterNameTag, Brushes.IndianRed, nameTag, sf);
                    }
                }
                // pos == 0 means draw under player
                else if (m.BoundingBox.Bottom < player.BoundingBox.Bottom - player.BoundingBox.Height /2 && pos == 0) 
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(190, 40, 40, 40)), _r);
                    g.DrawImage(m.CurrentSprite, m.ScreenX, m.ScreenY, m.Width, m.Height);

                    if (Options.showBoundingBox == true)
                    {
                        g.DrawRectangle(Pens.Red, m.BoundingBox);
                    }
                    if (showMonsterNames == true)
                    {
                            Rectangle nameTag = new Rectangle(m.ScreenX - (nameTagMaxWidth - m.Width) / 2, m.ScreenY - nameTagHeight - nameTagTextGap, nameTagMaxWidth, nameTagHeight);
                            g.DrawString(m.Name, MonsterNameTag, Brushes.IndianRed, nameTag, sf);
                    }
                }
            }
        }

        // Draws the damage numbers on top of the player
        private void DrawDamage(Graphics g)
        {
            //TODO - add this condition to the whole function
            //if (showDamageNumbers == true)
            //{
            //}

            // We create a copy of the original list, because we will both iterate through and modify it:
            List<int[]> _d = new List<int[]>(Monster.damageNumbers);

            // This is the displacement value when we remove items from the list, to avoid out of bounds exception.
            int _indxtra = 0;

            // To avoid wasting processing power when we don't have any damage to display:
            if (Monster.damageNumbers.Count > 0)
            foreach (var dmg in _d)
            {
                int _ind = _d.IndexOf(dmg) + _indxtra;
                Rectangle _dmgBox = new Rectangle(player.ScreenX - 5, player.ScreenY - dmg[1], player.Width + 10, 25);

                g.DrawString(dmg[0].ToString(), PlayerNameTag, Brushes.Red, _dmgBox, sf);

                Monster.damageNumbers[_ind][1]++;

                if (Monster.damageNumbers[_ind][1] > fps*4)
                {
                    Monster.damageNumbers.Remove(dmg);
                    _indxtra--;
                }
            }
        }

        private static void SpawnMonsters()
        {
            var _r = new Random();
            if (Monster.Counter < _monsterLimit)
            {
            Type _t = Monster.MonsterList[_r.Next(Monster.MonsterList.Count)];
                object m = Activator.CreateInstance(_t);
            }
        }

        private void SpawnCarrot()
        {
            if (carrots.Count < _carrotsLimit)
            {
                Item newCarrot = Item.SpawnCarrot(gameMap);
                carrots.Add(newCarrot);
            }
        }

        public void CollectCarrots()
        {
            Rectangle playerBoundingBox = player.BoundingBox;

            foreach (var carrot in carrots)
            {
                if (!carrot.IsCollected && playerBoundingBox.IntersectsWith(carrot.BoundingBox))
                {
                    carrot.IsCollected = true;
                    Player.currentPlayer.Carrots += 1;
                    carrot.ItemCarrotCollected();
                }
            }
        }

        private void DisposeOfAssets()
        {
            Monster.SpawnedMonsters.Clear();
            Monster.Counter = 0;
        }
        private void GameScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            bgm.StopAudioBackgroud();
            DisposeOfAssets();
            MainMenu m = new MainMenu();
            m.Show();
        }
    }
}

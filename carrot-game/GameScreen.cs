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
        private static int _monsterLimit = 3;

        // List of items carrots
        private List<Item> carrots = new List<Item>();
        private static int _carrotsLimit = 3;

        private Map gameMap;

        UIPlayer uIPlayer = new UIPlayer();

        Player heroCharacter = new Player();
        internal static Player P
        {
            get
            {
                return Player.currentPlayer;
            }
        }

        public int savePos;
        public GameScreen gs; 

        // Declaring a refresh rate of 30 frames per second [33.33ms] (1000ms / 30)
        public static int fps = 30;
        public static int refreshTime = 1000/fps;
        public static int monsterSpawnTimer = 0;

        Audio bgm = new Audio();

        // Declaring the name tag variables:
        public static bool showPlayerName = true;
        public static bool showMonsterNames = true;

        private static readonly StringFormat sf = new StringFormat();
        private readonly Font PlayerNameTag = new Font("Georgia", 14, FontStyle.Bold, GraphicsUnit.Point);
        private readonly Font MonsterNameTag = new Font("Georgia", 12, GraphicsUnit.Point);

        public GameScreen()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = new Size(ScreenWidth, ScreenHeight);
            gs = this;

            if (Options.bgm)
            bgm.PlayAudioBackgroud(bgm.AudioBackgroundPhase1);

            CreateMap();
            Program.CurrentScreen = "Game Screen";

            // Adjusting name tag text and alignment:
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;  
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

            // Deal with collisions
            heroCharacter.AllowMovement();
            foreach (var m in Monster.SpawnedMonsters)
            {
                if (heroCharacter.IsColliding(m))
                    heroCharacter.RestrictMovement(m);
            }
            // updates the character's position and sprite image.
            heroCharacter.Update();
            SpawnMonsters();
            //m.FollowPlayer(ref heroCharacter);
            AnimateMonsters();
            UpdateMonsters(ref Monster.SpawnedMonsters);

            // Spawn a new carrot every x seconds
            monsterSpawnTimer++;
            if (monsterSpawnTimer % (fps * 10) == 0 )
            {
                SpawnCarrot();
            }

            // Check and update carrot collection
            CollectCarrots();
        }

        private void AnimateMonsters()
        {
            foreach (Monster _m in  Monster.SpawnedMonsters)
            {
                _m.FollowPlayer(ref heroCharacter);
            }
        }

        private void PaintObjects(object sender, PaintEventArgs e)
        {
            Rectangle _pr = new Rectangle(heroCharacter.BoundingBox.Location.X - 5, heroCharacter.BoundingBox.Location.Y + heroCharacter.BoundingBox.Height - 20, heroCharacter.BoundingBox.Width + 10, 25);
            // Create a Graphics object to draw on the form
            Graphics g = e.Graphics;

            // Draw carrots
            foreach (var c in carrots)
            {
                if (!c.IsCollected)
                {
                    g.DrawImage(c.CarrotImage, c.PosX, c.PosY, c.Width, c.Height);
                }
            }

            // Draw monsters with lower z-index:
            DrawMonsters(g, 0);

            // Draw our Hero's Shadow:
            g.DrawEllipse(Pens.Black, _pr);
            g.FillEllipse(new SolidBrush(Color.FromArgb(190, 40, 40, 40)), _pr);
            // Draw our Hero:
            g.DrawImage(heroCharacter.CurrentSprite, heroCharacter.PosX, heroCharacter.PosY, heroCharacter.Width, heroCharacter.Height);
            if (Options.showBoundingBox == true)
            {
                g.DrawRectangle(new Pen(Color.Magenta, 3f), P.BoundingBox);
            }
            if (showPlayerName == true)
            {
                Rectangle nameTag = new Rectangle(heroCharacter.PosX, heroCharacter.PosY - 20, heroCharacter.Width, 20);
                g.DrawString(heroCharacter.Name, PlayerNameTag, Brushes.Black, nameTag, sf);
            }
            // Draw monsters with higher z-index:
            DrawMonsters(g, 1);

            // Draw damage numbers:
            DrawDamage(g);
        }

        private void PaintMap(object sender, PaintEventArgs e)
        {
            gameMap.Draw(e.Graphics);
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
            bgm.StopAudioBackgroud();
            DisposeOfAssets();
            MainMenu m = new MainMenu();
            m.Show();
        }

        private void DisposeOfAssets()
        {
            Monster.SpawnedMonsters.Clear();
            Monster.Counter = 0;
        }

        private void UpdateMonsters(ref List<Monster> monsters)
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
                Rectangle _r = new Rectangle(m.BoundingBox.Location.X - 5, m.BoundingBox.Location.Y + m.BoundingBox.Height - 20, m.BoundingBox.Width + 10, 25);

                if (m.BoundingBox.Bottom >= heroCharacter.BoundingBox.Bottom - heroCharacter.BoundingBox.Height/2 && pos == 1) // 1 meaning draw on top of player
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(190, 40, 40, 40)), _r);
                    g.DrawImage(m.CurrentSprite, m.PosX, m.PosY, m.Width, m.Height);
                    if (Options.showBoundingBox == true)
                    {
                        g.DrawRectangle(Pens.Red, m.BoundingBox);
                    }
                    if (showMonsterNames == true)
                    {
                        Rectangle nameTag = new Rectangle(m.PosX - (nameTagMaxWidth - m.Width)/2, m.PosY - nameTagHeight - nameTagTextGap, nameTagMaxWidth, nameTagHeight);
                        g.DrawString(m.Name, MonsterNameTag, Brushes.IndianRed, nameTag, sf);
                    }
                }
                else if (m.BoundingBox.Bottom < heroCharacter.BoundingBox.Bottom - heroCharacter.BoundingBox.Height /2 && pos == 0) // 0 meaning draw under player
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(190, 40, 40, 40)), _r);
                    g.DrawImage(m.CurrentSprite, m.PosX, m.PosY, m.Width, m.Height);

                    if (Options.showBoundingBox == true)
                    {
                        g.DrawRectangle(Pens.Red, m.BoundingBox);
                    }
                    if (showMonsterNames == true)
                    {
                            Rectangle nameTag = new Rectangle(m.PosX - (nameTagMaxWidth - m.Width) / 2, m.PosY - nameTagHeight - nameTagTextGap, nameTagMaxWidth, nameTagHeight);
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
                    Rectangle _dmgBox = new Rectangle(heroCharacter.PosX - 5, heroCharacter.PosY - dmg[1], heroCharacter.Width + 10, 25);

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
                Item newCarrot = Item.SpawnCarrot(ScreenWidth, ScreenHeight);
                carrots.Add(newCarrot);
            }
        }

        public void CollectCarrots()
        {
            Rectangle playerBoundingBox = heroCharacter.BoundingBox;

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

        private void CreateMap()
        {

            //Map data as a 2D array [22 rows and 40 col]
            int[,] mapData = new int[,]
            {
                {20,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19, 19, 19, 19},
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

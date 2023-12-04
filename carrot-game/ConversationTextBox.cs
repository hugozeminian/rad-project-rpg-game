using System;
using System.Drawing;
using System.Windows.Forms;

namespace carrot_game
{
    /// <summary>
    /// ConversationTextBox - Game conversation
    /// </summary>
    public class ConversationTextBox : TextBox
    {
        public ConversationTextBox(int screenWidth, int screenHeight)
        {
            this.Size = new Size(600, 100);
            //int xCoordinate = (screenWidth - this.Size.Width) / 2;
            //this.Location = new Point(xCoordinate, screenHeight - 2);
            this.Location = new Point(900, 20);
            this.Multiline = true;
            this.ReadOnly = true;
            this.Enabled = false;
            this.ScrollBars = ScrollBars.None;
            this.WordWrap = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = SystemColors.Control;
            this.ForeColor = SystemColors.ControlText;
            this.Font = new Font("Comic Sans MS", 16, FontStyle.Bold);
        }

        public void AddMessage((string Speaker, string Message) conversation)
        {
            if (string.IsNullOrEmpty(conversation.Speaker))
            {
                this.AppendText($"{conversation.Message}{Environment.NewLine}");
            }
            else
            {
                this.AppendText($"{conversation.Speaker}: {conversation.Message}{Environment.NewLine}");
            }

        }

        public void ClearConversation()
        {
            this.Clear();
        }

        public void HideConversation()
        {
            this.Hide();
        }

        public void ShowConversation()
        {
            this.Show();
        }

        // Get the array of conversations
        public (string Speaker, string Message)[] GetConversations()
        {
            return new[]
            {
                //0
                ("", "Hello, this is the carrot game"),
                
                //1-3
                ("NPC", "Hi there! How can I help you?"),
                ("Player", "I'm exploring the game world."),
                ("NPC", "Watch out for monsters!"),
            };
        }

    }
}

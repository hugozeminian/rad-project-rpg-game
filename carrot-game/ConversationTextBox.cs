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
        private Label hintTextLabel;
        private Timer blinkTimer;

        public ConversationTextBox(int screenWidth, int screenHeight)
        {
            this.Size = new Size(600, 100);
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

            // Subscribe to the HandleCreated event
            this.HandleCreated += ConversationTextBox_HandleCreated;
        }

        private void ConversationTextBox_HandleCreated(object sender, EventArgs e)
        {
            //Label on the right botton indicating to press the button
            hintTextLabel = new Label();
            hintTextLabel.Text = "Press L";
            hintTextLabel.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);
            hintTextLabel.Size = new Size(70, 25);
            hintTextLabel.BackColor = SystemColors.Control;
            hintTextLabel.ForeColor = Color.Black;
            hintTextLabel.Location = new Point(this.Right - hintTextLabel.Width - 2, this.Bottom - hintTextLabel.Bottom - 2);

            // Add the label to the form and bring to the front
            this.Parent.Controls.Add(hintTextLabel);
            hintTextLabel.BringToFront();

            // Initialize and start the blinking timer
            blinkTimer = new Timer();
            blinkTimer.Interval = 500; // Set the blinking interval (in milliseconds)
            blinkTimer.Tick += BlinkTimer_Tick;
            blinkTimer.Start();
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            // Toggle the visibility of the additional label
            hintTextLabel.Visible = !hintTextLabel.Visible;
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
            if (blinkTimer != null)
            {
                blinkTimer.Stop();
            }

            if (hintTextLabel != null)
            {
                hintTextLabel.Visible = false;
            }

            this.Hide();
        }

        public void ShowConversation()
        {
            if (blinkTimer != null)
            {
                blinkTimer.Start();
            }

            if (hintTextLabel != null)
            {
                hintTextLabel.Visible = true;
            }
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

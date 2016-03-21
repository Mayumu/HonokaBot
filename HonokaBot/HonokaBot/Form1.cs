using System;
using System.Windows.Forms;

namespace HonokaBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_connection_Click(object sender, EventArgs e)
        {
            if (button_connection.Text == "Connect")
            {
                button_connection.Text = "Disconnect";
                textBox_inputChannelName.Enabled = false;
                Program.twitchBot = new TwitchBot(textBox_inputChannelName.Text);
                Program.twitchBot.connect();
                Program.osuIRC = new OsuIRC();
                Program.osuIRC.connect();
            }
            else
            {
                button_connection.Text = "Connect";
                textBox_inputChannelName.Enabled = true;
                Program.twitchBot.disconnect();
                Program.osuIRC.disconnect();
                Program.ping.Stop();
            }
        }

        //method to write into the server response richtextbox
        public void consolewrite(string text)
        {
            if (richTextBox_serverResponse.InvokeRequired)
            {
                richTextBox_serverResponse.Invoke((MethodInvoker)delegate { consolewrite(text); });
            }
            else
            {
                richTextBox_serverResponse.Text += text + "\n";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (button_connection.Text != "Connect")
            {
                Program.twitchBot.disconnect();
                Program.osuIRC.disconnect();
                Program.ping.Stop();
            }
        }

        //say something in chat
        private void button_twitchSay_Click(object sender, EventArgs e)
        {
            Program.twitchBot.say(textBox_TwitchSay.Text);
            textBox_TwitchSay.Text = "";
        }

        //accessor to the "Send the request to Twitch chat too" checkbox
        public bool checkBox_sendRequestToChatToo_check
        {
            get
            {
                return checkBox_sendRequestToChatToo.Checked;
            }
        }
    }
}

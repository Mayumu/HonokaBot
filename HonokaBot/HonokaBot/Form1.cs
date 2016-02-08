using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            }
            else
            {
                button_connection.Text = "Connect";
                textBox_inputChannelName.Enabled = true;
                Program.twitchBot.disconnect();
            }
        }

        //method to write into the server response richtextbox
        private void consolewrite(string text)
        {
            richTextBox_serverResponse.Text += text + "\n";
        }

        //delegate to write into the server response richtextbox
        public void delegate_write(string text)
        {
            if (richTextBox_serverResponse.InvokeRequired == true)
                richTextBox_serverResponse.Invoke((MethodInvoker)delegate { consolewrite(text); });

            else
                consolewrite(text);
        }

    }
}

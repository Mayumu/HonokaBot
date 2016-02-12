namespace HonokaBot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_channelName = new System.Windows.Forms.Label();
            this.textBox_inputChannelName = new System.Windows.Forms.TextBox();
            this.button_connection = new System.Windows.Forms.Button();
            this.richTextBox_serverResponse = new System.Windows.Forms.RichTextBox();
            this.labelTwitchIRCOutput = new System.Windows.Forms.Label();
            this.textBox_TwitchSay = new System.Windows.Forms.TextBox();
            this.button_twitchSay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_channelName
            // 
            this.label_channelName.AutoSize = true;
            this.label_channelName.Location = new System.Drawing.Point(6, 9);
            this.label_channelName.Name = "label_channelName";
            this.label_channelName.Size = new System.Drawing.Size(75, 13);
            this.label_channelName.TabIndex = 0;
            this.label_channelName.Text = "Channel name";
            // 
            // textBox_inputChannelName
            // 
            this.textBox_inputChannelName.Location = new System.Drawing.Point(87, 6);
            this.textBox_inputChannelName.Name = "textBox_inputChannelName";
            this.textBox_inputChannelName.Size = new System.Drawing.Size(353, 20);
            this.textBox_inputChannelName.TabIndex = 1;
            this.textBox_inputChannelName.Text = "#mayumu";
            // 
            // button_connection
            // 
            this.button_connection.Location = new System.Drawing.Point(446, 6);
            this.button_connection.Name = "button_connection";
            this.button_connection.Size = new System.Drawing.Size(84, 20);
            this.button_connection.TabIndex = 2;
            this.button_connection.Text = "Connect";
            this.button_connection.UseVisualStyleBackColor = true;
            this.button_connection.Click += new System.EventHandler(this.button_connection_Click);
            // 
            // richTextBox_serverResponse
            // 
            this.richTextBox_serverResponse.Location = new System.Drawing.Point(9, 53);
            this.richTextBox_serverResponse.Name = "richTextBox_serverResponse";
            this.richTextBox_serverResponse.ReadOnly = true;
            this.richTextBox_serverResponse.Size = new System.Drawing.Size(516, 205);
            this.richTextBox_serverResponse.TabIndex = 3;
            this.richTextBox_serverResponse.Text = "";
            // 
            // labelTwitchIRCOutput
            // 
            this.labelTwitchIRCOutput.AutoSize = true;
            this.labelTwitchIRCOutput.Location = new System.Drawing.Point(6, 37);
            this.labelTwitchIRCOutput.Name = "labelTwitchIRCOutput";
            this.labelTwitchIRCOutput.Size = new System.Drawing.Size(102, 13);
            this.labelTwitchIRCOutput.TabIndex = 4;
            this.labelTwitchIRCOutput.Text = "Twitch\'s IRC Output";
            // 
            // textBox_TwitchSay
            // 
            this.textBox_TwitchSay.Location = new System.Drawing.Point(12, 264);
            this.textBox_TwitchSay.Name = "textBox_TwitchSay";
            this.textBox_TwitchSay.Size = new System.Drawing.Size(440, 20);
            this.textBox_TwitchSay.TabIndex = 7;
            // 
            // button_twitchSay
            // 
            this.button_twitchSay.Location = new System.Drawing.Point(455, 262);
            this.button_twitchSay.Name = "button_twitchSay";
            this.button_twitchSay.Size = new System.Drawing.Size(70, 23);
            this.button_twitchSay.TabIndex = 8;
            this.button_twitchSay.Text = "Say";
            this.button_twitchSay.UseVisualStyleBackColor = true;
            this.button_twitchSay.Click += new System.EventHandler(this.button_twitchSay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 296);
            this.Controls.Add(this.button_twitchSay);
            this.Controls.Add(this.textBox_TwitchSay);
            this.Controls.Add(this.labelTwitchIRCOutput);
            this.Controls.Add(this.richTextBox_serverResponse);
            this.Controls.Add(this.button_connection);
            this.Controls.Add(this.textBox_inputChannelName);
            this.Controls.Add(this.label_channelName);
            this.Name = "Form1";
            this.Text = "HonokaBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_channelName;
        private System.Windows.Forms.TextBox textBox_inputChannelName;
        private System.Windows.Forms.Button button_connection;
        private System.Windows.Forms.RichTextBox richTextBox_serverResponse;
        private System.Windows.Forms.Label labelTwitchIRCOutput;
        private System.Windows.Forms.TextBox textBox_TwitchSay;
        private System.Windows.Forms.Button button_twitchSay;
    }
}


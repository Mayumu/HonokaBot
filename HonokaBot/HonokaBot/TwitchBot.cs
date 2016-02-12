using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;

namespace HonokaBot
{
    class TwitchBot
    {
        //TCP Connection objects
        StreamWriter writer;
        TcpClient client;
        NetworkStream stream;
        StreamReader reader;
        //Connection info, password in another class, channel taken from the constructor
        string server = "irc.twitch.tv";
        int port = 6667;
        string name = ConnectionInfo.twitchLogin;
        string pass = ConnectionInfo.twitchPass;
        string channel;
        //seperate thread to maintain the connection and read from server
        Thread listening;
        //list of commands
        List<chat_command> list_of_commands = new List<chat_command>();

        //struct of a chat_command
        struct chat_command
        {
            public string call;
            public string reply;

            public chat_command(string call, string reply)
            {
                this.call = call;
                this.reply = reply;
            }
        }

        //the constructor
        public TwitchBot(string channel)
        {
            this.channel = channel;
        }

        //tell a command to the server you're connected to
        private void tell_server(string tellwut)
        {
            writer.WriteLine(tellwut);
            writer.Flush();
        }

        //say something in chatroom you're in
        public void say(string saywut)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", channel, saywut);
            writer.Flush();
        }

        //connect to the server and join the channel
        public void connect()
        {
            //Connect
            client = new TcpClient(server, port);
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            //Join the channel
            tell_server("USER " + name + " 0 * :" + name);
            tell_server("PASS " + pass);
            tell_server("NICK " + name);
            tell_server("JOIN " + channel);
            say("Connected!");
            //grab the commands from the mysql server
            download_commands();
            //start the thread maintaining the connection
            listening = new Thread(maintain_connection);
            listening.Start();
        }

        //disconnect from the server
        public void disconnect()
        {
            listening.Abort();
            reader.Close();
            writer.Close();
            stream.Close();
            client.Close();
        }

        //checks who said the line
        private string who_says_that(string input)
        {
            return input.Substring(1, input.IndexOf("!") - 1);
        }

        //add a new command
        private void addCommand(string input)
        {
            if (who_says_that(input) == ConnectionInfo.twitchOwner)
            {
                string res;
                string meth = addCommandParser(input, out res);
                int i;
                for (i = 0; i < list_of_commands.Count; i++)
                {
                    if (list_of_commands[i].call == meth)
                    {
                        i = int.MaxValue - 1;
                    }
                }
                if (res != "" && meth != "" && i != int.MaxValue)
                {
                    addCommandToDB(meth, res);
                    list_of_commands.Add(new chat_command(meth, res));
                    say("Command " + meth + " added.");
                }
                else say("Command already exists~!");
            }
        }

        //adding the command to the DB
        private void addCommandToDB(string call, string reply)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConnectionInfo.mysqlLogin;
            //INSERT INTO `BotCommands` (`id`, `command`, `reply`) VALUES (NULL, '!hi', 'hello') <-- example insert
            MySqlCommand command = new MySqlCommand(@"INSERT INTO `BotCommands` (`id`, `command`, `reply`) VALUES (NULL ,  '" + call + @"',  '" + reply + @"');", connection);
            connection.Open();
            command.ExecuteNonQuery();
            if (connection != null)
            {
                connection.Close();
            }
        }

        //parse the addcommand string
        private string addCommandParser(string input, out string result)
        {
            int index = input.IndexOf(":!addcom ");
            if (index == -1)
            {
                result = "";
                return "";
            }
            StringBuilder newStr = new StringBuilder();
            newStr.Append(input);
            newStr.Remove(0, index + 9); // :!addcom length + space
            index = newStr.ToString().IndexOf(" ");
            if (index == -1 || newStr.Length < 3) // znak spacja znak
            {
                result = "";
                return "";
            }
            string method = newStr.ToString().Substring(0, index);
            newStr.Remove(0, index + 1);

            result = newStr.ToString(); ;
            return method;
        }

        //delete a command
        private void delCommand(string input)
        {
            if (who_says_that(input) == "mayumu")
            {
                string delwhat = delCommandParser(input);
                if (delwhat != "")
                {
                    delComFromDB(delwhat);
                    foreach (chat_command x in list_of_commands)
                    {
                        if (x.call == delwhat)
                        {
                            list_of_commands.Remove(x);
                            break;
                        }
                    }
                    say("Command " + delwhat + " removed.");
                }
            }
        }

        //parsing method for the !delcom input
        private string delCommandParser(string input)
        {
            int index = input.IndexOf(":!delcom ");
            if (index == -1)
            {
                return "";
            }
            StringBuilder newStr = new StringBuilder();
            newStr.Append(input);
            newStr.Remove(0, index + 9); // :!addcom length + space

            if (newStr.Length < 1) // znak spacja znak
            {
                return "";
            }
            string method = newStr.ToString();
            return method;
        }

        //delete the command from the db
        private void delComFromDB(string call)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConnectionInfo.mysqlLogin;
            MySqlCommand command = new MySqlCommand(@"DELETE FROM `BotCommands` WHERE `command` = '" + call + "';", connection);
            connection.Open();
            command.ExecuteNonQuery();
            if (connection != null)
            {
                connection.Close();
            }
        }

        //edit a command
        private void editCommand(string input)
        {
            if (who_says_that(input) == "mayumu")
            {
                string res;
                string meth = editCommandParser(input, out res);
                if (res != "" && meth != "")
                {
                    editCommandInDB(meth, res);
                    for (int i = 0; i < list_of_commands.Count; i++)
                    {
                        if (list_of_commands[i].call == meth)
                        {
                            list_of_commands[i] = new chat_command(meth, res);
                        }
                    }
                    say("Edited command " + meth + " to say: \"" + res + "\"");
                }
            }
        }

        //parse the input with an !editcom
        string editCommandParser(string input, out string result)
        {
            int index = input.IndexOf(":!editcom ");
            if (index == -1)
            {
                result = "";
                return "";
            }
            StringBuilder newStr = new StringBuilder();
            newStr.Append(input);
            newStr.Remove(0, index + 10); // :!editcom length + space
            index = newStr.ToString().IndexOf(" ");
            if (index == -1 || newStr.Length < 3) // znak spacja znak
            {
                result = "";
                return "";
            }
            string method = newStr.ToString().Substring(0, index);
            newStr.Remove(0, index + 1);

            result = newStr.ToString(); ;
            return method;
        }

        //edit the command in DB
        private void editCommandInDB(string call, string result)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConnectionInfo.mysqlLogin;
            MySqlCommand command = new MySqlCommand(@"UPDATE `BotCommands` " + "SET reply='" + result + @"' WHERE `command` = '" + call + "';", connection);
            connection.Open();
            command.ExecuteNonQuery();
            if (connection != null)
            {
                connection.Close();
            }
        }

        //download commands from the database
        private void download_commands()
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConnectionInfo.mysqlLogin;
            MySqlDataReader reader = null;
            MySqlCommand command = new MySqlCommand("SELECT * FROM `BotCommands`", connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_of_commands.Add(new chat_command(reader.GetString(1), reader.GetString(2)));
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        //method for maintaining the connection and parsing what server tells us
        private void maintain_connection() //maintain the connection and parse the reader
        {
            string command = "";
            while ((command = reader.ReadLine()) != null)
            {
                Program.form1.consolewrite(command);
                char let;
                string num;
                if (command.Contains("PRIVMSG " + channel + " :!addcom")) //!addcom detected
                {
                    addCommand(command);
                }
                else if (command.Contains("PRIVMSG " + channel + " :!delcom")) //!delcom detected
                {
                    delCommand(command);
                }
                else if (command.Contains("PRIVMSG " + channel + " :!editcom")) //!editcom detected
                {
                    editCommand(command);
                }
                else if(Requester.is_map_link(command, out let, out num))
                {
                    Requester.request(let, num);
                }
                else
                {
                    foreach (chat_command com in list_of_commands) //replying to the static chat commands
                    {
                        if (command.Contains("PRIVMSG " + channel + " :" + com.call))
                        {
                            say(com.reply);
                        }
                    }
                }
            }
        }
    }
}

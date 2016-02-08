using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

using MySql.Data;
using MySql.Data.MySqlClient;

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
        string name = "honokabot";
        string pass = ConnectionInfo.twitchPass;
        string channel;
        //seperate thread to maintain the connection and read from server
        Thread listening;

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
        private void say(string saywut)
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
            say("hi");
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

        //method for maintaining the connection and parsing what server tells us
        private void maintain_connection() //maintain the connection and parse the reader
        {
            string command = "";
            while ((command = reader.ReadLine()) != null)
            {
                Program.form1.delegate_write(command);
            }
        }
    }
}

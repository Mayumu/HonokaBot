using System.Net.Sockets;
using System.IO;

namespace HonokaBot
{
    class OsuIRC
    {
        //TCP Connection objects
        StreamWriter writer;
        TcpClient client;
        NetworkStream stream;
        //Connection info
        string server = "irc.ppy.sh";
        int port = 6667;
        string name = ConnectionInfo.osuUsername;
        string pass = ConnectionInfo.osuIRCPassword;

        public void connect()
        {
            client = new TcpClient(server, port);
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            tell_server("USER " + name + " 0 * :" + name);
            tell_server("PASS " + pass);
            tell_server("NICK " + name);
        }

        private void tell_server(string tellwut)
        {
            writer.WriteLine(tellwut);
            writer.Flush();
        }

        public void PM_Myself(string pmwut)
        {
            //writer.WriteLine("PRIVMSG " + name + " :" + pmwut);
            writer.WriteLine("PRIVMSG " + "daxyn" + " :" + pmwut);
            writer.Flush();
        }

        public void disconnect()
        {
            writer.Close();
            stream.Close();
            client.Close();
        }
    }
}

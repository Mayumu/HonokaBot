using System.Threading;

//Taken from http://stackoverflow.com/questions/30495849/tcpclient-disconnects-after-awhile-from-twitch-irc and modified

namespace HonokaBot
{
    class PingSender
    {
        static string PING = "PING ";
        private Thread pingSender;

        // Empty constructor makes instance of Thread
        public PingSender()
        {
            pingSender = new Thread(new ThreadStart(this.Run));
        }
        // Starts the thread
        public void Start()
        {
            pingSender.Start();
        }

        public void Stop()
        {
            pingSender.Abort();
        }
        // Send PING to irc server every 5 minutes
        public void Run()
        {
            while (true)
            {
                Program.twitchBot.tell_server(PING + "irc.twitch.tv");
                Thread.Sleep(300000); // 5 minutes
            }
        }
    }
}

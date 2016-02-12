using System;
using System.Windows.Forms;

namespace HonokaBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        //declaration of our bots objects
        public static TwitchBot twitchBot;
        public static OsuIRC osuIRC;

        //initial form - need to declare it here to make it accessible from all classes
        public static Form1 form1;

        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1();
            Application.Run(form1);
        }
    }
}

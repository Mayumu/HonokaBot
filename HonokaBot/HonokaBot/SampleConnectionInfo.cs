namespace HonokaBot
{
    class SampleConnectionInfo
    {
        //your twich login - must be lowercase
        public static string twitchOwner = "xxxxxxx";
        //your bot's twitch login - also must be lowercase
        public static string twitchLogin = "honokabot";
        //your bot's twitch password
        public static string twitchPass = "oauth:xxxxxxxxxxxxxxxxxxxxx";
        //your mysql database
        public static string mysqlLogin = "Server=xxxxxxxxxxxxxx; database=xxxxxxx; UID=xxxxxxx; password=xxxxxxx";
        //how to create table: CREATE TABLE `BotCommands` (`id` INT AUTO_INCREMENT PRIMARY KEY, `command` VARCHAR(20) NOT NULL, `reply` VARCHAR(250) NOT NULL)
        //your osu API key
        public static string osuAPIKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        //your osu login
        public static string osuUsername = "xxxxxxx";
        //your osu IRC password (it's different than your regular password!)
        public static string osuIRCPassword = "xxxxxxx";
    }
}

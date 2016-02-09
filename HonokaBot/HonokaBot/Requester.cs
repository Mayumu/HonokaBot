using Newtonsoft.Json;

namespace HonokaBot
{
    class Requester
    {
        //find out if the input contains a maplink, if so return true and out the letter and mapnumber
        public static bool is_map_link(string link, out char letter, out string mapnum)
        {
            letter = 'a';
            mapnum = "";
            if (link.Contains("osu.ppy.sh/s/") || link.Contains("osu.ppy.sh/b/"))
            {
                if (link.Contains("osu.ppy.sh/s/"))
                {
                    letter = 's';
                    int location = link.IndexOf("osu.ppy.sh/s/") + 13; //+12 to slash ostatni
                    mapnum = "";
                    if (link.Length >= location)
                    {
                        while (location < link.Length)
                        {
                            if (link[location] == '0' || link[location] == '1' || link[location] == '2' || link[location] == '3' || link[location] == '4' || link[location] == '5' || link[location] == '6' || link[location] == '7' || link[location] == '8' || link[location] == '9')
                            {
                                mapnum += link[location];
                                location++;
                            }
                            else location = int.MaxValue;
                        }
                        if (mapnum.Length > 0)
                        {
                            //request(letter, mapnum);
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                }
                else
                {
                    letter = 'b';
                    int location = link.IndexOf("osu.ppy.sh/b/") + 13; //+12 to slash ostatni
                    mapnum = "";
                    if (link.Length >= location)
                    {
                        while (location < link.Length)
                        {
                            if (link[location] == '0' || link[location] == '1' || link[location] == '2' || link[location] == '3' || link[location] == '4' || link[location] == '5' || link[location] == '6' || link[location] == '7' || link[location] == '8' || link[location] == '9')
                            {
                                mapnum += link[location];
                                location++;
                            }
                            else location = int.MaxValue;
                        }
                        if (mapnum.Length > 0)
                        {
                            //request(letter, mapnum);
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            else return false;
        }

        public static Beatmap[] getMapInfo(char letter, string mapnum)
        {
            string apilink = "https://osu.ppy.sh/api/get_beatmaps?k=" + ConnectionInfo.osuAPIKey + "&" + letter + "=" + mapnum;
            string apiresult = "[]";
            apiresult = new System.Net.WebClient().DownloadString(apilink);
            Beatmap[] deserializedData = JsonConvert.DeserializeObject<Beatmap[]>(apiresult);
            return deserializedData;
        }
    }
}

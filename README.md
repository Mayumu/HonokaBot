# HonokaBot
Twitch chat bot with the ability to read osu! map links and sending them in-game.

Current functionality:
 - joining any channel on twitch.tv
 - taking static commands and giving proper replies
 - adding new commands with "!addcom !commandcall command reply"
 - removing commands with "!delcom !commandcall"
 - editing existing commands with "!editcom !commandcall new command reply"
 - taking osu! beatmap links from chat, getting map info from the API and sending a private message to your osu! account

To make it work you need to fill in all the fields in the "SampleConnectionInfo.cs" class and rename it to "ConnectionInfo.cs".
It'll require you to have a Twitch.tv account as well as an osu! account.

http://twitch.tv - Twitch, a gaming livestreaming platform
http://osu.ppy.sh - osu!, a free rhythm game

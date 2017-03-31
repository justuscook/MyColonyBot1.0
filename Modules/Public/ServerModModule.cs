using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MyBot.Modules.Public
{
    public class ServerModModule : ModuleBase
    {
		public static SocketTextChannel adminChannel;
			[Command("setAdmin")][Remarks("Sets admin channel")][RequireUserPermission(GuildPermission.Administrator)]
		public async Task setAdmin(SocketTextChannel channel)
		{
			adminChannel = channel;
			await ReplyAsync("Admin channel set to: " + adminChannel.ToString());
		}
			[Command("join")][Remarks("When you join the server, you can send the server admins a message stating what group you are from")]
		public async Task join([Remainder] string text)
		{
			if (adminChannel == null)
			{
				await ReplyAsync("An admin needs to set the admin channel first.");
			}
			else
			{ 
				await adminChannel.SendMessageAsync(Context.User + " said: " + text);
				await ReplyAsync("Thanks for subbmitting your information " + "**" + Context.User.Username + "**"+ ", the admins will review it and assign your role soon!");
			}
			//adminChannel = await Context.Guild.GetDefaultChannelAsync() as SocketChannel;		
		}
        [Command("hiswitch")][Remarks("Toggle on and off the -hi command")] [RequireUserPermission(GuildPermission.Administrator)]
        public async Task hiswitch()
        {
			string toggle;
			var result = Database.CheckExistingServer(Context.Guild.Id);
            if (result.Count() <= 0)
            {
                Database.EnterServer(Context.Guild.Id, Context.Guild.Name);
             }
			result = Database.CheckHiSwitch(Context.Guild.Id);
			if (result.First() == "0")
			{
				toggle = "1";
				Database.ToggleHiSwitch(Context.Guild.Id, toggle);
			}
			else
			{
				toggle = "0";
				Database.ToggleHiSwitch(Context.Guild.Id, toggle);
			}
			if (toggle == "1")
			{
				await ReplyAsync("Your have toggled the -hi command: **ON**");
			}
			else await ReplyAsync("Your have toggled the -hi command: **OFF**");

		}
		[Command("memeswitch")]
		[Remarks("Toggle on and off the -meme command")]
		[RequireUserPermission(GuildPermission.Administrator)]
		public async Task Memeswitch()
		{
			string toggle;
			var result = Database.CheckExistingServer(Context.Guild.Id);
			if (result.Count() <= 0)
			{
				Database.EnterServer(Context.Guild.Id, Context.Guild.Name);
			}
			result = Database.CheckMemeSwitch(Context.Guild.Id);
			if (result.First() == "0")
			{
				toggle = "1";
				Database.ToggleMemeSwitch(Context.Guild.Id, toggle);
			}
			else
			{
				toggle = "0";
				Database.ToggleMemeSwitch(Context.Guild.Id, toggle);
			}
			if (toggle == "1")
			{
				await ReplyAsync("Your have toggled the -meme command: **ON**");
			}
			else await ReplyAsync("Your have toggled the -meme command: **OFF**");

		}
		[Command("beermeswitch")]
		[Remarks("Toggle on and off the -beerme command")]
		[RequireUserPermission(GuildPermission.Administrator)]
		public async Task beerMeswitch()
		{
			string toggle;
			var result = Database.CheckExistingServer(Context.Guild.Id);
			if (result.Count() <= 0)
			{
				Database.EnterServer(Context.Guild.Id, Context.Guild.Name);
			}
			result = Database.CheckBeerMeSwitch(Context.Guild.Id);
			if (result.First() == "0")
			{
				toggle = "1";
				Database.ToggleBeerMeSwitch(Context.Guild.Id, toggle);
			}
			else
			{
				toggle = "0";
				Database.ToggleBeerMeSwitch(Context.Guild.Id, toggle);
			}
			if (toggle == "1")
			{
				await ReplyAsync("Your have toggled the -beerme command: **ON**");
			}
			else await ReplyAsync("Your have toggled the -beerme command: **OFF**");

		}
	}
}

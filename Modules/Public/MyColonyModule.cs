using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Runtime.InteropServices;
using System.Diagnostics;
using cloudscribe.HtmlAgilityPack;

namespace MyBot.Modules.Public
{

	public class MyColonyModules : ModuleBase
	{
        [Command("stat")]
        [Alias("stats")]
        [Remarks("Type -stat @SomeUser and get their stats, can also mention yourself, but thats kinda showing off, eh?")]
        public async Task stat(SocketUser user)
        {
            string charter = Database.CheckCharterCode(user.Id).First();
            if (charter == null || charter == "")
            {
                await ReplyAsync("That user doesn't have a charter code set yet, sorry man.");
            }
            else
            {
                var obj = new HtmlWeb();
                await ReplyAsync("Let me look that up, one moment.");
                HtmlDocument colony = obj.Load("https://www.my-colony.com/colonies/" + charter);
                string screenshot = colony.DocumentNode.SelectSingleNode("//div[@id='colonyHeading']").GetAttributeValue("style", null);//screenshot
                string name = colony.DocumentNode.SelectSingleNode("//h2").InnerText;//Name
                string CW = colony.DocumentNode.SelectSingleNode("//div[@id='commonwealthHeader']").InnerText;//CW
                string founded = colony.DocumentNode.SelectSingleNode("//div[@id='colonyHeadingFounding']").InnerText;//founded
                string pop = colony.DocumentNode.SelectSingleNode("//div[@id='colonyHeadingPopulation']").InnerText;//Pop
                string GDP = colony.DocumentNode.SelectSingleNode("//div[@id='colonyEconGDP']").InnerText;//GDP
                string unEm = colony.DocumentNode.SelectSingleNode("//div[@id='colonyEconUnemployment']").InnerText;//unemployment
                string RRR = "";

                if (name == "")
                {
                    await ReplyAsync("Silly, thats not a valid charter code, try again with another charter code.");
                }
                else
                {


                    if (CW.Replace("Commonwealth of ", "") == name)
                    {
                        CW = "Commonwealth Capital";
                        if (name == "United Earth")
                        {
                            RRR = "RRR Index: Over 9000!!!";
                        }
                        else
                        {
                            RRR = colony.DocumentNode.SelectSingleNode("//div[@id='rrrIndexArea']").InnerText.Trim();
                        }
                    }
                    else
                    {
                        CW = "A " + CW;
                    }
                    name = "Colony: " + name;

                    var embed = new EmbedBuilder();

                    if (screenshot != null)
                    {
                        screenshot = screenshot.Substring(22);
                        screenshot = screenshot.Substring(0, screenshot.Length - 2);
                        screenshot = "https://www.my-colony.com" + screenshot + "\n";
                        embed.ImageUrl = screenshot;
                    }
                    embed.WithColor(new Color(0x4900ff))
                    .AddField(y =>
                    {
                        y.Name = name;
                        string output = CW + "\n" + founded + "\n";
                        if (RRR != "")
                        {
                            output = output + RRR + "\n";
                        }
                        output += pop + "\n" + GDP + "\n" + unEm + "\n";
                        y.Value = output;
                        y.IsInline = false;
                    });
                    await ReplyAsync("", embed: embed);
                }
            }
        }
        [Command("stat")]
		[Alias("stats")]
		[Remarks("Type -stat <your charter code> to get stats from yout My Colony webpage")]
		public async Task stat(string charter = null, string check = null)
		{
            if(charter == null)
            {
                charter = Database.CheckCharterCode(Context.User.Id).First();
                await ReplyAsync("Using your charter from the database: **" + charter + "**");
            }
			var obj = new HtmlWeb();
			await ReplyAsync("Let me look that up, one moment.");
			HtmlDocument colony = obj.Load("https://www.my-colony.com/colonies/" + charter);
			string screenshot = colony.DocumentNode.SelectSingleNode("//div[@id='colonyHeading']").GetAttributeValue("style", null);//screenshot
			string name = colony.DocumentNode.SelectSingleNode("//h2").InnerText;//Name
			string CW = colony.DocumentNode.SelectSingleNode("//div[@id='commonwealthHeader']").InnerText;//CW
			string founded = colony.DocumentNode.SelectSingleNode("//div[@id='colonyHeadingFounding']").InnerText;//founded
			string pop = colony.DocumentNode.SelectSingleNode("//div[@id='colonyHeadingPopulation']").InnerText;//Pop
			string GDP = colony.DocumentNode.SelectSingleNode("//div[@id='colonyEconGDP']").InnerText;//GDP
			string unEm = colony.DocumentNode.SelectSingleNode("//div[@id='colonyEconUnemployment']").InnerText;//unemployment
			string RRR = "";

			if (name == "")
			{
				await ReplyAsync("Silly, thats not a valid charter code, try again with another charter code.");
			}
			else
			{
				if(check == "set")
					{
					var result = Database.CheckExistingUser(Context.User.Id.ToString());
					if (result.Count() <= 0)
					{
                        Database.EnterUser(Context.User.Id, Context.User.ToString());
                        Database.EnterCharterCode(Context.User.Id.ToString(), charter);
					}
                    else Database.EnterCharterCode(Context.User.Id.ToString(), charter);
                    await ReplyAsync("Your charter code **" + charter + "** has been added to the database, you just have to type -stat now, no need for your charter code.");
                }
                
				if (CW.Replace("Commonwealth of ", "") == name)
				{
					CW = "Commonwealth Capital";
					if (name == "United Earth")
					{
						RRR = "RRR Index: Over 9000!!!";
					}
					else
					{
						RRR = colony.DocumentNode.SelectSingleNode("//div[@id='rrrIndexArea']").InnerText.Trim();
					}
				}
				else
				{
					CW = "A " + CW;
				}
				name = "Colony: " + name;

				var embed = new EmbedBuilder();

				if (screenshot != null)
				{
					screenshot = screenshot.Substring(22);
					screenshot = screenshot.Substring(0, screenshot.Length - 2);
					screenshot = "https://www.my-colony.com" + screenshot + "\n";
					embed.ImageUrl = screenshot;
				}
				embed.WithColor(new Color(0x4900ff))
				.AddField(y =>
				{
					y.Name = name;
					string output = CW + "\n" + founded + "\n";
					if (RRR != "")
					{
						output = output + RRR + "\n";
					}
					output += pop + "\n" + GDP + "\n" + unEm + "\n";
					y.Value = output;
					y.IsInline = false;
				});
				await ReplyAsync("", embed: embed);
			}
		}
        
    }
}
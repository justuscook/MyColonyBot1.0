using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace MyBot
{
	public class Program
	{
		// Convert our sync main to an async main.
		public static void Main(string[] args) =>
			new Program().Start().GetAwaiter().GetResult();

		private DiscordSocketClient client;
        private CommandHandler handler;

		public async Task Start()
		{
			// Define the DiscordSocketClient
			client = new DiscordSocketClient();

			var token = "xxxxxxxxxx";

			// Login and connect to Discord.
			await client.LoginAsync(TokenType.Bot, token);
			await client.StartAsync();
			client.Ready += async () =>
			{
				client.SetGameAsync($"-help | In guilds: {client.Guilds.Count}");
			};
			client.JoinedGuild += async (e) =>
			{
				client.SetGameAsync($"-help | In guilds: {client.Guilds.Count}");
            };
			client.LeftGuild += async (e) =>
			{
				client.SetGameAsync($"-help | In guilds: {client.Guilds.Count}");
			};
			var map = new DependencyMap();
			map.Add(client);

			handler = new CommandHandler();
			await handler.Install(map);
			Console.WriteLine($"{DateTime.UtcNow}: YourBot initiated...");
            

			// Block this program until it is closed.
			await Task.Delay(-1);
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}

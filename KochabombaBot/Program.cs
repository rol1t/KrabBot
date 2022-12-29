using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using KochabombaBot.Commons;

namespace KochabombaBot
{
    internal class Program
    {
        private DiscordSocketClient _client;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var config = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            };

            _client = new DiscordSocketClient(config);
            _client.MessageReceived += CommandsHandler;
            _client.Log += _client_Log;
            
            // your token
            var token = "";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            string command = string.Empty;

            while (command != "exit")
            {
                command = Console.ReadLine();
            }
        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandsHandler(SocketMessage arg)
        {
            var commands = CommonMethod.GetCommands();

            foreach (var command in commands)
            {
                command.Start(arg, _client);
            }

            return Task.CompletedTask;
        }
    }
}

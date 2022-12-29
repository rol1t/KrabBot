using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KochabombaBot.Commands
{
    public class AnalCarnavalCommand : SpamCommands
    {
        public override List<string> _commands { get; set; } = new List<string>()
        {
            "!total"
        };

        public override string Description() => "Тотальный спам";

        public override Task Execute()
        {
            var args = GetAttributes(_msg.Content);

            var messages = new List<string>();
            var channels = _client.Guilds.SelectMany(g => g.Channels);
            foreach (var item in channels)
            {
                var channel = _client.GetChannel(item.Id) as IMessageChannel;
                for (int i = 0; i < args.times; i++)
                {
                    if (channel != null)
                    {
                        Task.WaitAll(Task.Delay(args.interval).ContinueWith(t => { _msg.Channel.SendMessageAsync(args.message); }));
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}

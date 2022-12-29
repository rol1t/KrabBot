using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KochabombaBot.Commands
{
    public class SpamCommands : BaseCommand
    {
        private const double defaultInterval = 0.1;

        private const int defaultCount = 10;

        private const string defaultText = "@everyone";

        private List<string> _attributes = new List<string>() 
        {
            "times",
            "interval"
        };

        public override List<string> _commands { get; set; } = new List<string>()
        {
            "!spam"
        };

        public override string Description()
        {
            return "Спамит сообщение. " +
                @"```Шаблон: !spam [message=@everyonе] [times=50]```";
        }

        public override Task Execute()
        {
            var commands = _msg.Content.Split();

            var messages = new List<string>();


            if (!_commands.Contains(commands[0]) || _msg.Author.IsBot)
            {
                return Task.CompletedTask;
            }

            if (commands.Count() == 1)
            {
                for (int i = 0; i < defaultCount; i++)
                {
                    _msg.Channel.SendMessageAsync(defaultText);
                }

                return Task.CompletedTask;
            }

            //var attibutes = GetAttributes(commands.ToList());

            for (int i = 0; i < defaultCount; i++)
            {
                var message = commands.TakeLast(commands.Length - 1).ToArray();
                _msg.Channel.SendMessageAsync(string.Join(' ', message));
            }

            return Task.CompletedTask;
        }

        private (int times, double interval) GetAttributes(List<string> words)
        {
            var attributes = words.Where(word => string.Join(' ', _attributes).Contains(word));
            var times = defaultCount;
            var interval = defaultInterval;


            foreach (var item in attributes)
            {
                var attribute = _attributes.Where(a => a.Contains(item)).FirstOrDefault();

                if (attribute != null)
                    continue;

                var items = attribute.Split('=');

                switch (items[0])
                {
                    case "interval":
                        interval = double.TryParse(item, out var i) == true ? i : interval;
                        break;
                    case "times":
                        times = int.TryParse(items[1], out var t) == true ? t : times;
                        break;
                    default:
                        break;
                }
            }
            return (times, interval);
        }
    }
}

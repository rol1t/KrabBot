using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KochabombaBot.Commands
{
    public class SpamCommands : BaseCommand
    {
        public override List<string> _commands { get; set; } = new List<string>()
        {
            "!spam"
        };

        public override string Description()
        {
            return "Спамит сообщение. " +
                @"```Шаблон: !spam --@everyonе --times=50```";
        }

        public override Task Execute()
        {
            var args = GetAttributes(_msg.Content);

            var messages = new List<string>();

            for (int i = 0; i < args.times; i++)
            {
                Task.WaitAll(Task.Delay(args.interval).ContinueWith(t => { _msg.Channel.SendMessageAsync(args.message); }));
            }

            return Task.CompletedTask;
        }

        protected (string message, int times, int interval) GetAttributes(string message)
        {
            var attributes = message.Substring(4).Split("--", StringSplitOptions.RemoveEmptyEntries).Skip(1);
            return GetCommandsFromString(attributes.ToList());
        }

        private (string message, int times, int interval) GetCommandsFromString(List<string> strings)
        {
            var message = "@everyone";
            var times = 10;
            var interval = 1;

            foreach (var str in strings)
            {
                if (Enum.TryParse(typeof(Arguments), str[0].ToString(), true, out var tmpType))
                {
                    var argType = (Arguments)tmpType;
                    switch (argType)
                    {
                        case Arguments.m:
                            message = str[1..];
                            break;
                        case Arguments.t:
                            if (!int.TryParse(str[1..], out times))
                            {
                                times = 10;
                            }
                            break;
                        case Arguments.i:
                            if (!int.TryParse(str[1..], out interval))
                            {
                                interval = 1;
                            }
                            break;
                    }
                }
            }
            return (message, times, interval);
        }

        /// <summary>
        /// m - message
        /// t - times
        /// i - interval
        /// </summary>
        enum Arguments
        {
            m,
            t,
            i
        }
    }
}

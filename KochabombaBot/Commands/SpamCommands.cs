using System.Collections.Generic;
using System.Linq;
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
                _msg.Channel.SendMessageAsync(args.message);
            }

            return Task.CompletedTask;
        }

        private (string message, int times) GetAttributes(string message)
        {
            var attributes = message.Split(' ').TakeLast(2);

            if (attributes.Count() == 1)
            {
                return (attributes.FirstOrDefault(), 10);
            }

            if (attributes.Count() != 2)
            {
                return ("@everyone", 5);
            }

            if (!int.TryParse(attributes.LastOrDefault(), out var times))
            {
                times = 10;
            }

            return (attributes.FirstOrDefault(), times);
        }
    }
}

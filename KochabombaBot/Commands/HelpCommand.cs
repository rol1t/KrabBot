using KochabombaBot.Commons;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KochabombaBot.Commands
{
    public class HelpCommand : BaseCommand
    {
        public override List<string> _commands { get; set; } = new List<string>()
        {
            "!help",
            "!помощь"
        };

        public override string Description()
        {
            return "**Просмотр доступных комманд.**";
        }

        public override Task Execute()
        {
            var commands = CommonMethod.GetCommands();
            StringBuilder sb = new StringBuilder();

            foreach (var command in commands)
            {
                var descr = command.Description();
                var commandWords = command.AvailableCommand();

                sb.AppendLine(descr);
                foreach (var commandText in commandWords)
                {
                    sb.AppendLine("\t\t" + commandText);
                }
            }

            _msg.Channel.SendMessageAsync(sb.ToString());

            return Task.CompletedTask;
        }
    }
}

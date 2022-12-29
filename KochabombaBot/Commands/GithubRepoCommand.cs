using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KochabombaBot.Commands
{
    public class GithubRepoCommand : BaseCommand
    {
        public override List<string> _commands { get; set; } = new List<string>()
        {
            "!repo",
            "!github"
        };

        public override string Description() => "Ссылка на репозиторий";

        public override Task Execute()
        {
            return _msg.Channel.SendMessageAsync("https://github.com/rol1t/KrabBot");
        }
    }
}

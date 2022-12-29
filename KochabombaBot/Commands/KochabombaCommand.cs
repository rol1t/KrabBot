using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KochabombaBot.Commands
{
    public class KochabombaCommand: BaseCommand
    {  
        public override List<string> _commands { get; set; } = new List<string>()
        {
            "!кочабамба",
            "!kochabamba",
            "!kocha",
            "!cochabamba"
        };

        public override string Description()
        {
            return "Время по Кочабамба (Боливия)";
        }

        public override Task Execute()
        {
            var date = DateTime.UtcNow.AddHours(-4);
            var text = string.Empty;

            if (date.Hour == 11)
            {
                text = $"Сейчас в Кочабамбе (Боливия) {date} пора @everyone";
            }
            else
            {
                text = $"Сейчас в Кочабамбе (Боливия) {date}";
            }

            _msg.Channel.SendMessageAsync(text);

            return Task.CompletedTask;
        }
    }
}

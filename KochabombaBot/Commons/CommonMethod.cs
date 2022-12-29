using KochabombaBot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KochabombaBot.Commons
{
    public static class CommonMethod
    {
        public static List<BaseCommand> GetCommands()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(s => s.IsSubclassOf(typeof(BaseCommand)));

            var commands = new List<BaseCommand>();

            foreach (var type in types)
            {
                commands.Add((BaseCommand)Activator.CreateInstance(type));
            }

            return commands;
        }
    }
}

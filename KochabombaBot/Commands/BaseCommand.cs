using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KochabombaBot.Commands
{
    public abstract class BaseCommand
    {
        public BaseSocketClient _client;
        public SocketMessage _msg;

        /// <summary>
        /// Add your command here
        /// </summary>
        public abstract List<string> _commands { get; set; }

        /// <summary>
        /// Type your command here
        /// </summary>
        /// <returns>command task</returns>
        public abstract Task Execute();

        /// <summary>
        /// Verify command content
        /// default: check user is bot and command contains in message
        /// </summary>
        /// <returns>Verify status</returns>
        public virtual bool Verify()
        {
            if (_msg.Author.IsBot)
            {
                return false;
            }

            foreach (var command in _commands)
            {
                if (_msg.Content.ToLower().Contains(command))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// List of available command
        /// </summary>
        /// <returns></returns>
        public virtual List<string> AvailableCommand()
        {
            return _commands;
        }

        public abstract string Description();

        public virtual Task Start(SocketMessage msg, BaseSocketClient client)
        {
            _client = client;
            _msg = msg;

            if (Verify())
            {
                return Execute();
            } 
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}

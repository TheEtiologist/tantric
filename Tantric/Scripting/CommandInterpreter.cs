using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Scripting
{
    // Interprets commands
    // Commands are basically String to Callback assosciations
    public class CommandInterpreter
    {
        Dictionary<String, Command> m_RegisteredCommands;

        public List<Command> Commands
        {
            get
            {
                return new List<Command>(m_RegisteredCommands.Values);
            }
        }

        public CommandInterpreter()
        {
            m_RegisteredCommands = new Dictionary<string, Command>();
        }

        // Register the command for future use
        public void RegisterCommand(Command command)
        {
            if (command != null && m_RegisteredCommands != null && !m_RegisteredCommands.ContainsKey(command.Keyword))
                m_RegisteredCommands.Add(command.Keyword, command);
        }

        //Evaluate the preregistered Command
        public void Evaluate(String command)
        {
            String Keyword = command.Split(" ".ToCharArray(), StringSplitOptions.None)[0];
            if (m_RegisteredCommands != null && m_RegisteredCommands.ContainsKey(Keyword.Trim()))
                m_RegisteredCommands[Keyword.Trim()].Invoke(command);

        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Scripting
{
    /// <summary>
    /// A Composition of several commands, some of which could be scripts
    /// </summary>
    public class Script : Command
    {
        // Commands to be run
        List<String> m_Commands;
        // Interpreter object to run them on
        CommandInterpreter m_Interpreter;

        // Constructor : initialize default variables
        public Script(CommandInterpreter interpreter, String fileName, String name) : base(name, 1, null)
        {
            m_Commands = new List<string>();
            m_Interpreter = interpreter;
            this.Callback = this.Execute;
            StreamReader file = new StreamReader(fileName);
            String line = "";
            String line2 = "";
            bool Found = false;
            // Specifically, parse the script in itself
            while (!Found)
            {
                line2 = file.ReadLine().Trim();
                if (line2 == null)
                    return;
                if (line == name)
                {
                    if (line2 == "{")
                        Found = true;
                }
                else
                    line = line2;
            }

            ArgumentCount = line.Split(" ".ToCharArray(), StringSplitOptions.None).Length - 1;
            
            line = file.ReadLine().Trim();
            while (!line.Contains("}"))
            {
                // Add each lin that isn't a comment to m_Commands
                if (!line.StartsWith("//", StringComparison.OrdinalIgnoreCase))
                    m_Commands.Add(line);
                line = file.ReadLine().Trim();
            }

        }

        // Simply tell the interpreter to run each command
        public void Execute(List<String> arguments)
        {
            foreach (String Command in m_Commands)
            {
                m_Interpreter.Evaluate(Command);
            }
        }
    }
}

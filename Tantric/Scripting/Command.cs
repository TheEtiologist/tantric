using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Scripting
{
    public delegate void CommandCallback(List<String> arguments);
    // Represents a name, argument, callback mapping
    public class Command
    {
        private string m_Name;
        /// <summary>
        /// Minimum number of arguments.  Might use more than this
        /// </summary>
        private int m_ArgumentCount;
        private CommandCallback m_Callback;

        public String Keyword
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public int ArgumentCount
        {
            get
            {
                return m_ArgumentCount;
            }
            protected set
            {
                m_ArgumentCount = value;
            }
        }

        public CommandCallback Callback
        {
            get
            {
                return m_Callback;
            }
            protected set
            {
                m_Callback = value;
            }
        }

        public Command(String name, int arguments, CommandCallback callback)
        {
            m_Name = name;
            m_ArgumentCount = arguments;
            m_Callback = callback;
        }

        

        public void Invoke(String commandLine)
        {
            List<String> arguments = new List<String>(commandLine.Split(" ".ToCharArray()));
            try
            {
                if (arguments.Count < m_ArgumentCount)
                    throw new Exception();
                arguments = arguments.GetRange(1, arguments.Count-1);
            }
            catch (Exception)
            {
                throw new ArgumentException("Not enough arguments for command " + Keyword);
            }
            m_Callback(arguments);
        }
    }
}

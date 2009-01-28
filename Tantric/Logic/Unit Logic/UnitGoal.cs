using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Logic
{
    public class UnitGoal
    {
        private String m_Name;
        private List<Object> m_Arguments;
        private bool m_Exclusive;

        public String Name
        {
            get
            {
                return m_Name;
            }
        }

        public Boolean Exclusive
        {
            get
            {
                return m_Exclusive;
            }
        }

        public Object GetArgument(int argumentNumber)
        {
            return m_Arguments[argumentNumber];
        }

        public void Satisfy()
        {
            m_Name = "Satisfied";
        }

        public UnitGoal(String name, List<Object> arguments, Boolean exclusive)
        {
            m_Arguments = new List<object>();
            m_Name = name;
            m_Exclusive = exclusive;
            m_Arguments.AddRange(arguments);
        }

        public UnitGoal(String name,Boolean exclusive, params object[] arguments)
        {
            m_Arguments = new List<object>();
            m_Name = name;
            m_Exclusive = exclusive;
            foreach (object obj in arguments)
                m_Arguments.Add(obj);
        }
    }
}

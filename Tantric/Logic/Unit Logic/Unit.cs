using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Tantric.Logic
{
    public abstract class Unit : Tantric.World.WorldObject
    {
        private List<UnitGoal> m_Goals;
        private IGoalInterpreter m_GoalInterp;

        public List<UnitGoal> Goals
        {
            get
            {
                return m_Goals;
            }
        }

        public IGoalInterpreter GoalInterpreter
        {
            get
            {
                return m_GoalInterp;
            }
        }

        public void AddGoal(UnitGoal goal)
        {
            if (goal.Exclusive && ContainsGoal(goal.Name))
            {
                if ((int)goal.GetArgument(0) < 0 || (int)goal.GetArgument(0) > (int)GetGoal(goal.Name).GetArgument(0))
                {
                    RemoveGoal(goal.Name);
                }
                else
                    return;
            }
            m_Goals.Add(goal);
        }

        public void RemoveGoal(UnitGoal goal)
        {
            if (m_Goals.Contains(goal))
                m_Goals.Remove(goal);
        }

        public void RemoveGoal(String name)
        {
            for (int x = 0; x < m_Goals.Count; x++)
            {
                if (m_Goals[x].Name == name)
                {
                    m_Goals.RemoveAt(x);
                    x++;
                }
            }
        }

        public bool ContainsGoal(String name)
        {
            foreach(UnitGoal g in m_Goals)
                if( g.Name.ToUpper(CultureInfo.InvariantCulture) == name.ToUpper(CultureInfo.InvariantCulture) )
                    return true;
            return false;
        }

        public UnitGoal GetGoal(String name)
        {

            foreach (UnitGoal g in m_Goals)
                if (g.Name.ToUpper(CultureInfo.InvariantCulture) == name.ToUpper(CultureInfo.InvariantCulture))
                    return g;
            return null;
        }

        protected Unit(String name, IGoalInterpreter interpreter) : base(name)
        {
            m_GoalInterp = interpreter;
            m_Goals = new List<UnitGoal>();
        }
    }
}

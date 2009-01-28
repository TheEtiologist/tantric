using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Logic
{
    public interface IGoalInterpreter
    {
        void InterpretGoal(Unit unit, UnitGoal goal, int elapsedMilliseconds);
    }
}

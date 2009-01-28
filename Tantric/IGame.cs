using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric
{
    public interface IGame
    {
        World.IObjectFactory ObjFactory
        {
            get;
        }

        Logic.GamePlayConductor GamePlayConductor
        {
            get;
        }

        Logic.IGoalInterpreter GoalInterpreter
        {
            get;
        }
    }
}

using System;
using System.Collections.Generic;
using Tantric;
namespace Tantric_Testbed
{
    static class TantricDemo
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using ( Engine game = new Engine(new TestbedGame()))
            {
                game.IsMouseVisible = true;
                game.Run();
            }
        }
    }

    public class TestbedGame : Tantric.IGame
    {
        TestbedFactory m_Factory;
        TestbedGameplay m_Gameplay;
        HumanGoalInterpreter m_GoalInterpreter;

        public TestbedGame()
        {
            m_Factory = new TestbedFactory(this);
            m_Gameplay = new TestbedGameplay();
            m_GoalInterpreter = new HumanGoalInterpreter();
            World.Objects.Human.HumanStatistics.LoadStatistics("");
        }

        public Tantric.World.IObjectFactory ObjFactory
        {
            get { return m_Factory; }
        }

        public Tantric.Logic.GamePlayConductor GamePlayConductor
        {
            get { return m_Gameplay; }
        }

        public Tantric.Logic.IGoalInterpreter GoalInterpreter
        {
            get { return m_GoalInterpreter; }
        }
    }
}


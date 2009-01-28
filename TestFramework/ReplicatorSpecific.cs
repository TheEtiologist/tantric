using System;
using System.Collections.Generic;
using System.Text;

namespace Tantric_Testbed
{
    public class TestbedGameplay : Tantric.Logic.GamePlayConductor
    {
        List<Tantric.Logic.Unit> m_Selected;
        Tantric.Graphical.Camera m_Camera;

        public TestbedGameplay()
        {
            m_Selected = new List<Tantric.Logic.Unit>();
        }

        public override void Evaluate(Microsoft.Xna.Framework.GameTime gt)
        {
            m_Selected.Clear();
            foreach (Tantric.World.WorldLayer layer in Orchestra.Layers)
                foreach (Tantric.World.WorldObject obj in layer.Objects.Values)
                    if (obj is World.Objects.Human.HumanAssembler)
                        m_Selected.Add(obj as World.Objects.Human.HumanAssembler);
            if (m_Camera == null && Orchestra != null)
                m_Camera = Orchestra.Camera;
        }

        public override void ProcessInput(string Pressed, Microsoft.Xna.Framework.Vector2 Mouse)
        {
            // TODO: Clean up formation algorithm
            #region Formation
            if (m_Selected != null && Pressed == "Mouse_Right" && m_Camera != null)
            {
                Microsoft.Xna.Framework.Vector2 offset = new Microsoft.Xna.Framework.Vector2();
                Microsoft.Xna.Framework.Vector2 formationOrientation = new Microsoft.Xna.Framework.Vector2();
                Microsoft.Xna.Framework.Vector2 flippedOrientation = new Microsoft.Xna.Framework.Vector2();
                Microsoft.Xna.Framework.Vector2 clickedLocation = (m_Camera.Position + Mouse);
                float averageX = 0;
                float averageY = 0;
                int numberUnits = m_Selected.Count;
                int rows = 4;
                int spacing = 100;
                float counterX = -.5f * (Math.Min(numberUnits, rows) - 1);
                if (counterX < rows * -.5f)
                    counterX = rows * -.5f;
                float counterY = 0;
                foreach (Tantric.Logic.Unit unit in m_Selected)
                {
                    averageX += unit.Position.X;
                    averageY += unit.Position.Y;
                }
                averageX /= numberUnits;
                averageY /= numberUnits;

                formationOrientation = clickedLocation - new Microsoft.Xna.Framework.Vector2(averageX, averageY);
                formationOrientation.Normalize();
                flippedOrientation.X = -formationOrientation.Y;
                flippedOrientation.Y = formationOrientation.X;

                foreach (Tantric.Logic.Unit unit in m_Selected)
                {
                    offset = (flippedOrientation * counterX) * spacing;
                    offset += -formationOrientation * counterY * spacing;
                    unit.AddGoal(new Tantric.Logic.UnitGoal("MoveTo", true, -1, clickedLocation + offset));
                    counterX += 1;
                    if (counterX > (rows - 1) * .5)
                    {
                        numberUnits -= rows;
                        counterX = -.5f * (Math.Min(numberUnits, rows) - 1);
                        if (counterX < rows * -.5f)
                            counterX = rows * -.5f;
                        counterY += 1;
                    }
                }
            }
            #endregion
            if (m_Camera != null && Pressed == "Camera_Left")
                m_Camera.Translate(new Microsoft.Xna.Framework.Vector2(-1, 0) * World.Objects.Human.HumanStatistics.GetStatistic("Camera", "Speed"));
            if (m_Camera != null && Pressed == "Camera_Right")
                m_Camera.Translate(new Microsoft.Xna.Framework.Vector2(1, 0) * World.Objects.Human.HumanStatistics.GetStatistic("Camera", "Speed"));
            if (m_Camera != null && Pressed == "Camera_Up")
                m_Camera.Translate(new Microsoft.Xna.Framework.Vector2(0, -1) * World.Objects.Human.HumanStatistics.GetStatistic("Camera", "Speed"));
            if (m_Camera != null && Pressed == "Camera_Down")
                m_Camera.Translate(new Microsoft.Xna.Framework.Vector2(0, 1) * World.Objects.Human.HumanStatistics.GetStatistic("Camera", "Speed"));
        }
    }

    public class TestbedFactory : Tantric.World.IObjectFactory
    {
        protected TestbedGame m_Game;

        public TestbedFactory(TestbedGame g)
        {
            m_Game = g;
        }

        public Tantric.World.WorldObject Create(string Type, List<String> Arguments)
        {
            switch (Type)
            {
                case "PurelyVisual":
                    return new World.Objects.PurelyVisual(Arguments[0], Arguments[1]);
                case "HumanAssembler":
                    return new World.Objects.Human.HumanAssembler(Arguments[0], Arguments[1], m_Game.GoalInterpreter);
                default:
                    break;
            }
            return null;
        }
    }

    public class HumanGoalInterpreter : Tantric.Logic.IGoalInterpreter
    {
        public void InterpretGoal(Tantric.Logic.Unit u, Tantric.Logic.UnitGoal goal, int elapsedMilliseconds)
        {
            switch (goal.Name.ToLower())
            {
                case "moveto": // Move To is exclusive
                    Microsoft.Xna.Framework.Vector2 dir =  (Microsoft.Xna.Framework.Vector2)goal.GetArgument(1) - u.Position;
                    Microsoft.Xna.Framework.Vector2 norm = dir;
                    norm.Normalize();
                    norm *= elapsedMilliseconds;
                    norm /= 1000;
                    norm *= World.Objects.Human.HumanStatistics.GetStatistic("Human_Assembler", "Speed");
                    if (dir.LengthSquared() == 0)
                    {
                        goal.Satisfy();
                        break;
                    }
                    if (norm.LengthSquared() > dir.LengthSquared())
                    {
                        u.Translate(dir);
                        goal.Satisfy();
                    }
                    else
                        u.Translate(norm);
                    break;
                default:
                    break;
            }
        }
    }
}

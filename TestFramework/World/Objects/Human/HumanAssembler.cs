using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tantric_Testbed.World.Objects.Human
{
    public class HumanAssembler : Tantric.Logic.Unit
    {
        protected String m_Visual;
        protected Texture2D m_Graphic;
        protected Vector2 m_Origin;

        public HumanAssembler(String name, String visual, Tantric.Logic.IGoalInterpreter interp) : base(name, interp)
        {
            m_Visual = visual;
        }

        public override void Update(int elapsedMilliseconds)
        {
            RemoveGoal("Satisfied");
            foreach(Tantric.Logic.UnitGoal goal in Goals)
                GoalInterpreter.InterpretGoal(this, goal, elapsedMilliseconds);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Tantric.Graphical.Camera cam)
        {
            batch.Draw(m_Graphic, Position - cam.Position, null, Color.White, (float)Rotation - (float)cam.Rotation, m_Origin, (float)ScaleFactor.X, SpriteEffects.None, 1.0f);
        }

        public override void LoadResources(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            m_Graphic = content.Load<Texture2D>(m_Visual);
            m_Origin = new Vector2(m_Graphic.Width / 2, m_Graphic.Height / 2);
        }

        public override void UnloadResources()
        {
        }
    }
}

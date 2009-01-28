using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tantric_Testbed.World.Objects
{
    public class PurelyVisual : Tantric.World.WorldObject
    {
        Microsoft.Xna.Framework.Graphics.Texture2D m_Graphic;
        String m_VisualName;
        Microsoft.Xna.Framework.Vector2 m_Origin;
        public PurelyVisual(String Name, String Visual) : base(Name)
        {
            m_VisualName = Visual;
        }



        public override void Update(int elapsedMilliseconds)
        {
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Tantric.Graphical.Camera cam)
        {
            batch.Draw(m_Graphic, Position - cam.Position, null, Color.White, (float)Rotation - (float)cam.Rotation, m_Origin, (float)ScaleFactor.X, SpriteEffects.None, 1.0f);
        }

        public override void LoadResources(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            m_Graphic = content.Load<Texture2D>(m_VisualName);
            m_Origin = new Vector2(m_Graphic.Width / 2, m_Graphic.Height / 2);
        }

        public override void UnloadResources()
        {
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;

//namespace Replicators.World.Objects
//{
//    public class BackgroundObj : WorldObject
//    {
//        protected String m_Graphic;

//        public BackgroundObj( String Graphic) : base("Background")
//        {
//            m_Position = Vector2.Zero;
//            m_Scale = 1.0;
//            m_Rotation = 0;
//            m_Graphic = Graphic;
//        }

//        public override void Update(Microsoft.Xna.Framework.GameTime gt)
//        {
//        }

//        public override void RegisterLayer(WorldLayer lay)
//        {
//            lay.AddVisual(new WorldVisual(this, m_Graphic, "", Vector2.Zero, Vector2.One, 0.0));
//        }
//    }
//}

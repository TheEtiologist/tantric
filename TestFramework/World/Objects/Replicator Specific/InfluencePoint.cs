//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Replicators.Graphical;

//namespace Replicators.World.Objects
//{
//    public class InfluencePoint : WorldObject
//    {
//        protected double m_InfluenceFactor;

//        public double Influence 
//        {
//            get
//            {
//                return m_InfluenceFactor;
//            }
//            set
//            {
//                m_InfluenceFactor = value;
//            }
//        }

//        public InfluencePoint() : base( "Influence" + SceneManager.Identifier )
//        {
//            m_InfluenceFactor = 1.0;
//        }

//        public InfluencePoint(String name) : base(name)
//        {
//            m_InfluenceFactor = 1.0;
//        }

//        public override void Update(Microsoft.Xna.Framework.GameTime gt)
//        {
//        }

//        public override void RegisterLayer(WorldLayer lay)
//        {
//        }
//    }
//}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace Replicators.World
//{
//    public class ReplicatorVisual : WorldVisual
//    {
//        Texture2D Static;
//        public ReplicatorVisual() : base(null, "Static", "Replicators", Vector2.Zero, Vector2.One, 0.0)
//        {
//            Static = null;
//        }

//        public override void _LoadResources(Microsoft.Xna.Framework.Content.ContentManager content)
//        {
//            Static = content.Load<Texture2D>("Static");
//        } 

//        public override void Draw(GameTime gt, SpriteBatch batch, double Depth, Replicators.Graphical.Camera cam)
//        {
//            this.EffectParameters["CameraPos"].SetValue(cam.Position);
//            this.EffectParameters["Timer"].SetValue(gt.TotalGameTime.Ticks);
//            base.Draw(gt, batch, Depth, new Replicators.Graphical.Camera());
//        }
//    }
//}

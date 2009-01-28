using System;
using System.Collections.Generic;
using System.Text;

namespace Tantric_Testbed.World.Objects.Human
{
    class HumanBase : Tantric.World.WorldObject
    {
        public HumanBase(String name) : base(name)
        {

        }

        public override void Update(int elapsedMilliseconds)
        {
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Tantric.Graphical.Camera cam)
        {
        }

        public override void LoadResources(Microsoft.Xna.Framework.Content.ContentManager content)
        {
        }

        public override void UnloadResources()
        {
        }
    }
}

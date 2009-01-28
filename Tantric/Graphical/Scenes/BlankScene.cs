using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Tantric.Graphical.Scenes
{
    public class BlankScene : Scene
    {

        public BlankScene(GraphicsDeviceManager dm, ContentManager content, String name) : base(dm, content, name)
        {
        }

        protected override void InternallyLoadResources()
        {
        }

        protected override void InternallyUnloadResources()
        {
        }

        public override void Pause()
        {
        }

        public override void Unpause()
        {
        }

        public override void Logic(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Chime();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Microsoft.Xna.Framework.Graphics.Color.Black);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }
    }
}

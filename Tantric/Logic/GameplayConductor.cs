using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Tantric.Logic
{
    // Gameplay conductor needs to:
    // Evaulate the situation
    // Evaulate one piece of input
    public class GamePlayConductor
    {
        private Graphical.Scenes.WorldScene m_Orchestra;

        protected Graphical.Scenes.WorldScene Orchestra
        {
            get
            {
                return m_Orchestra;
            }
        }

        public GamePlayConductor()
        {
        }

        public void Prep(Graphical.Scenes.WorldScene scene)
        {
            m_Orchestra = scene;
        }

        public virtual void Evaluate(GameTime gameTime)
        {
            // Do nothing
        }

        public virtual void ProcessInput(String pressed, Vector2 mouse)
        {
            // Do nothing
        }
    }
}

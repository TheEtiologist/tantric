using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Logic
{
    class PhysicsConductor
    {
        Graphical.Scenes.WorldScene m_Orchestra;

        public PhysicsConductor(Graphical.Scenes.WorldScene scene)
        {
            m_Orchestra = scene;
        }
    }
}

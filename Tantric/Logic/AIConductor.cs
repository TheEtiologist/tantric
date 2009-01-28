using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Logic
{
    class AIConductor
    {
        Graphical.Scenes.WorldScene m_Orchestra;

        public AIConductor(Graphical.Scenes.WorldScene scene)
        {
            m_Orchestra = scene;
        }
    }
}

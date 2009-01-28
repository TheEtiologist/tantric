using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.World
{
    public interface IObjectFactory
    {
        WorldObject Create(String type, List<String> arguments);
    }
}

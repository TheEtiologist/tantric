using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tantric.World
{
    interface IPositionable
    {
        Vector2 Position
        {
            get;
        }

        Vector2 ScaleFactor
        {
            get;
        }

        double Rotation
        {
            get;
        }

        void Translate(Vector2 delta);

        void Rotate(double delta);

        void Scale(Vector2 Factor);

        void SetPosition(Vector2 pos);

        void SetRotation(double rot);

        void SetScale(Vector2 sca);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Tantric.World;

namespace Tantric.Graphical
{
    public class Camera : IPositionable
    {
        private Vector2 m_Position;
        private Vector2 m_Scale;
        private double m_Rotation;
        private Boolean m_Changed;

        private Matrix m_Transform;

        public Camera()
        {
            m_Position = new Vector2(0f, 0f);
            m_Scale = new Vector2(1f, 1f);
            m_Changed = true;
        }

        public Vector2 Position 
        {
            get 
            {
                return m_Position;
            }
        }

        public Vector2 ScaleFactor
        {
            get
            {
                return m_Scale;
            }
        }

        public double Rotation
        {
            get
            {
                return m_Rotation;
            }
        }

        public Matrix Transform
        {
            get
            {
                if (m_Changed)
                {
                    m_Transform = Matrix.CreateRotationZ((float)m_Rotation) * Matrix.CreateScale(m_Scale.X, m_Scale.Y, 1.0f) * Matrix.CreateTranslation(m_Position.X, m_Position.Y, 0.0f);
                    m_Changed = false;
                }
                return m_Transform;
            }
        }

        public void Translate(Vector2 delta)
        {
            m_Position += delta;
            m_Changed = true;
        }

        public void Rotate(double delta)
        {
            m_Rotation += delta;
            m_Changed = true;
        }

        public void Scale(Vector2 Factor)
        {
            m_Scale *= Factor;
        }

        public void SetPosition(Vector2 pos)
        {
            m_Position = pos;
        }

        public void SetRotation(double rot)
        {
            m_Rotation = rot;
        }

        public void SetScale(Vector2 sca)
        {
            m_Scale = sca;
        }

    }
}

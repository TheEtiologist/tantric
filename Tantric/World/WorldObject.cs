using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Tantric.Graphical;

namespace Tantric.World
{
    /// <summary>
    /// Represents an object in a layer with a transform and graphic
    /// </summary>
    public abstract class WorldObject : IPositionable
    {
        /// <summary>
        /// Transformation
        /// </summary>
        private Vector2 m_Position;
        private double m_Rotation;
        private double m_Scale;

        private String m_Name;

        public String Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        protected WorldObject(String name)
        {
            m_Name = name;
            m_Scale = 1.0f;
        }

        /// <summary>
        /// Update based on the objects logic.  Override.
        /// </summary>
        /// <param name="gt">Time elapsed</param>
        public abstract void Update(int elapsedMilliseconds);

        /// <summary>
        /// Draw the object.  Override.
        /// </summary>
        /// <param name="gt"></param>
        /// <param name="batch"></param>
        /// <param name="cam"></param>
        public abstract void Draw(GameTime gameTime, SpriteBatch batch, Camera cam);

        public abstract void LoadResources(ContentManager content);
        public abstract void UnloadResources();

        #region IPositionable Members

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
                return new Vector2((float)m_Scale);
            }
        }

        public double Rotation
        {
            get
            {
                return m_Rotation;
            }
        }

        public void Translate(Vector2 delta)
        {
            m_Position += delta;
        }

        public void Rotate(double delta)
        {
            m_Rotation += delta;
        }

        public void Scale(Vector2 Factor)
        {
            m_Scale *= Factor.Length();
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
            m_Scale = sca.Length();
        }

        #endregion
    }
}

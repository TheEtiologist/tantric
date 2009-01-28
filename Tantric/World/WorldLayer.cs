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
    /// Represents one layer of the world.  Maintains a list of objects,
    /// provides an interface for transfer, and performs collision detection
    /// and response within the layer.
    /// </summary>
    public class WorldLayer
    {
        private String m_LayerName;
        private double m_Depth;
        private Dictionary<String, WorldObject> m_Objects;

        public String LayerName 
        {
            get
            {
                return m_LayerName;
            }
            set
            {
                m_LayerName = value;
            }
        }

        public double Depth
        {
            get
            {
                return m_Depth;
            }
            set
            {
                m_Depth = value;
            }
        }

        public Dictionary<String, WorldObject> Objects
        {
            get
            {
                return m_Objects;
            }
        }

        public WorldObject GetObject(String name)
        {
            foreach(WorldObject obj in m_Objects.Values)
                if( obj.Name == name)
                    return obj;
            return null;
        }

        //public Dictionary<String, WorldVisual> GetVisuals()
        //{
        //    return m_Visuals;
        //}

        //public WorldObject this[string Name]
        //{
        //    get
        //    {
        //        return m_Objects[Name];
        //    }
        //}

        public WorldLayer(String name)
        {
            m_LayerName = name;
            m_Objects = new Dictionary<string, WorldObject>();
            //m_Visuals = new Dictionary<string, WorldVisual>();
            //m_Drivers = new List<WorldDriver>();
            m_Depth = 1.0f;
        }

        public void AddObject(WorldObject obj)
        {
            m_Objects.Add(obj.Name, obj);
        }

        //public void AddVisual(WorldVisual vis)
        //{
        //    m_Visuals.Add(vis.Name, vis);
        //}

        //public void AddDriver(WorldDriver drive)
        //{
        //    m_Drivers.Add(drive);
        //}

        public void LoadResources(ContentManager content)
        {
            // Load object resources
            foreach (WorldObject obj in m_Objects.Values)
            {
                obj.LoadResources(content);
            }
        }

        public void Update(int elapsedMilliseconds)
        {
            foreach (WorldObject obj in m_Objects.Values)
            {
                obj.Update(elapsedMilliseconds);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch batch, Camera cam)
        {
            foreach (WorldObject obj in m_Objects.Values)
                obj.Draw(gameTime, batch, cam);
        }
    }
}

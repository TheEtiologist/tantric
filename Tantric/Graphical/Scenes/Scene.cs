using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tantric.Graphical
{
    /// <summary>
    /// Represents a segment of the game that needs to be drawn,
    /// with it's own resources to be loaded and destroyed.
    /// </summary>
    public abstract class Scene : Trash.ITrashable
    {
        private GraphicsDeviceManager m_Graphics;
        private ContentManager m_Content;
        private Boolean m_Done;
        private Boolean m_ResourcesLoaded;
        private String m_Name;

        public Boolean Loaded 
        {
            get
            {
                return m_ResourcesLoaded;
            }
        }

        public Boolean Done 
        { 
            get
            {
                return m_Done;
            }
            protected set
            {
                m_Done = value;
            }
        }

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

        public ContentManager Content
        {
            get
            {
                return m_Content;
            }
        }

        public GraphicsDeviceManager Graphics
        {
            get
            {
                return m_Graphics;
            }
        }


        protected Scene(GraphicsDeviceManager manager, ContentManager content, String name )
        {
            m_Graphics = manager;
            m_Content = content;
            m_Name = name;
        }

        /// <summary>
        /// Load the resources we need.
        /// </summary>
        public void LoadResources()
        {
            if (m_Done == false)
            {
                Unpause();
                InternallyLoadResources();
                m_ResourcesLoaded = true;
            }
        }

        /// <summary>
        /// Internal resource load.
        /// </summary>
        protected abstract void InternallyLoadResources();

        /// <summary>
        /// Unload the resources we're using.
        /// </summary>
        public void UnloadResources()
        {
            if (m_Done == false)
                Pause();
            InternallyUnloadResources();
            m_ResourcesLoaded = false;
        }

        protected abstract void InternallyUnloadResources();

        /// <summary>
        /// Pause execution of the scene
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// Unpause execution of the scene.
        /// </summary>
        public abstract void Unpause();

        /// <summary>
        /// Update the scene.
        /// </summary>
        /// <param name="elapsedTime"></param>
        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Logic(gameTime);
        }

        public abstract void HandleInput(GameTime gameTime);
        public abstract void Logic(GameTime gameTime);

        /// <summary>
        /// Draw the scene.
        /// </summary>
        /// <param name="gt"></param>
        public abstract void Draw(GameTime gameTime);


        #region ITrashable Members

        Trash.CleanHandler _Clean;
        bool _Chimed;

        public Tantric.Trash.CleanHandler Clean
        {
            get
            {
                return _Clean;
            }
            set
            {
                _Clean = value;
            }
        }

        public bool Chimed
        {
            get 
            {
                if (_Chimed)
                {
                    _Chimed = false;
                    return true;
                }
                return false;
            }
        }

        protected void Chime()
        {
            _Chimed = true;
        }

        #endregion
    }
}

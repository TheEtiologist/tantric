using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
/*
namespace Tantric.Graphical
{
    /// <summary>
    /// A series of scenes run in sequence
    /// </summary>
    class Sequence : Scene
    {
        /// <summary>
        /// Scenes to be run in series
        /// </summary>
        protected Queue<Scene> m_Scenes;

        public Sequence(GraphicsDeviceManager manager, ContentManager content, String name) : base(manager, content, name)
        {
            m_Scenes = new Queue<Scene>();
        }

        /// <summary>
        /// Add a scene
        /// </summary>
        /// <param name="sc">Scene to add</param>
        public void AddScene(Scene sc)
        {
            if (m_Scenes != null && sc != null)
                m_Scenes.Enqueue(sc);
        }

        /// <summary>
        /// Load the resources of the current scene
        /// </summary>
        protected override void _LoadResources()
        {
            if (m_Scenes.Count > 0)
                m_Scenes.Peek().LoadResources();
        }

        /// <summary>
        /// Unload the resources of the current scene
        /// </summary>
        protected override void _UnloadResources()
        {
            if (m_Scenes.Count > 0)
                m_Scenes.Peek().UnloadResources();
        }

        /// <summary>
        /// Pause the current scene
        /// </summary>
        public override void Pause()
        {
            if (m_Scenes.Count > 0)
                m_Scenes.Peek().Pause();
        }

        /// <summary>
        /// Unpause the current scene
        /// </summary>
        public override void Unpause()
        {
            if (m_Scenes.Count > 0)
                m_Scenes.Peek().Unpause();
        }

        /// <summary>
        /// Handle input of current scene
        /// </summary>
        /// <param name="gt">Game time</param>
        public override void _HandleInput(Microsoft.Xna.Framework.GameTime gt)
        {
            if (m_Scenes.Count > 0)
                m_Scenes.Peek()._HandleInput(gt);
        }

        /// <summary>
        /// Update logic of current scene
        /// </summary>
        /// <param name="gt">Game time</param>
        public override void _Logic(Microsoft.Xna.Framework.GameTime gt)
        {
            if (m_Scenes.Peek().Done)
            {
                m_Scenes.Dequeue().UnloadResources();
                m_Scenes.Peek().LoadResources();
                m_Scenes.Peek().Clean = this.CleanScene;
                Engine.Instance.TrashCompactor.AddTrash(m_Scenes.Peek());
            }
            if(m_Scenes.Count > 0 )
                m_Scenes.Peek()._Logic(gt);
        }

        public void CleanScene(Trash.ITrashable trash)
        {
            Scene sc = (Scene)trash;
            if (sc != null && m_Scenes.Contains(sc))
            {
                sc.Pause();
                sc.UnloadResources();
                if (sc == m_Scenes.Peek())
                    m_Scenes.Dequeue();
                else
                    throw new Exception("Trash Collector has inactive scene from sequence.");
            }
        }

        /// <summary>
        /// Draw the current scene
        /// </summary>
        /// <param name="gt">Game time</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gt)
        {
            m_Scenes.Peek().Draw(gt);
        }
    }
}
*/
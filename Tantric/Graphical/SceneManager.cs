using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tantric.Graphical
{
    /// <summary>
    /// Represents the currently queued list of scenes
    /// </summary>
    public class SceneManager
    {
        /// <summary>
        /// The scenes which are enqueued.
        /// </summary>
        List<Scene> m_Scenes;

        /// <summary>
        /// Scene Identifier counter
        /// </summary>
        /// <remarks>
        /// Used to provide a unique ID to each scene, increasing it by one each time etc.
        /// </remarks>
        private static int m_IdentifierSeed;

        /// <summary>
        /// Maximum number of scenes running at once
        /// </summary>
        /// <remarks>
        /// Used for performance reasons
        /// </remarks>
        private int m_MaximumScenes = 10;

        public List<Scene> Scenes
        {
            get
            {
                return m_Scenes;
            }
        }

        /// <summary>
        /// Unique Identifier for your scene
        /// </summary>
        public static int UniqueSceneId
        {
            get
            {
                return ++m_IdentifierSeed;
            }
        }

        /// <summary>
        /// Maximum number of allowed running scenes.
        /// </summary>
        public int MaximumScenes
        {
            get
            {
                return m_MaximumScenes;
            }
            set
            {
                m_MaximumScenes = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SceneManager()
        {
            m_Scenes = new List<Scene>();
        }

        /// <summary>
        /// Add a scene to the queue.
        /// </summary>
        /// <param name="sc">Scene to enqueue</param>
        public void AddScene(Scene scene)
        {
            // Simply add to the end of the queue.
            if (scene != null && m_Scenes != null && m_Scenes.Count < MaximumScenes)
            {
                m_Scenes.Add(scene);
                scene.Clean = this.CleanScene;
                Engine.Instance.TrashCompactor.AddTrash(scene);
            }
        }

        public Scene GetScene(String name)
        {
            foreach (Scene sc in m_Scenes)
                if (sc.Name == name)
                    return sc;
            return null;
        }

        public void CleanScene(Trash.ITrashable trash)
        {
            Scene sc = (Scene)trash;
            if (sc != null && m_Scenes.Contains(sc))
            {
                sc.Pause();
                sc.UnloadResources();
                m_Scenes.Remove(sc);
            }
        }

        /// <summary>
        /// Lost focus, so unload all resources in the current scene.
        /// </summary>
        public void LoseFocus()
        {
            if (m_Scenes != null)
                if (m_Scenes.Count > 0)
                    foreach (Scene sc in m_Scenes)
                        sc.UnloadResources();
        }

        /// <summary>
        /// Gained focus, so load all resources in the current scene.
        /// </summary>
        public void GainFocus()
        {
            if (m_Scenes != null)
                if (m_Scenes.Count > 0)
                    foreach (Scene sc in m_Scenes)
                        sc.UnloadResources();
        }

        /// <summary>
        /// Update our current scene.
        /// </summary>
        /// <param name="gt">Current Game time. (It's Game time!)</param>
        public void Update( GameTime gameTime )
        {
            if (m_Scenes != null && m_Scenes.Count > 0)
            {
                foreach (Scene sc in m_Scenes)
                {
                    if (!sc.Loaded)
                        // Load the resources if we need too
                        sc.LoadResources();
                    if (!sc.Done)
                        // Update if it's not done,
                        sc.Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (m_Scenes != null && m_Scenes.Count > 0)
            {
                foreach (Scene sc in m_Scenes)
                {
                    if (!sc.Loaded)
                        // Load the resources if we need too
                        sc.LoadResources();
                    if (!sc.Done)
                        // Update if it's not done,
                        sc.Draw(gameTime);
                }
            }
        }
    }
}

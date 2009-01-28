using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tantric.Trash
{
    /// <summary>
    /// System to clean up unresponsive/unused items.
    /// </summary>
    public sealed class TrashCompactor
    {
        // List of items to monitor
        private List<ITrashable> m_Trashables;
        // Milliseconds elapsed
        private double m_Milliseconds;
        // How long before I start cleaning objects?
        private double m_Interval;

        public List<ITrashable> Trashables
        {
            get
            {
                return m_Trashables;
            }
        }

        public double Interval
        {
            get
            {
                return m_Interval;
            }
            set
            {
                m_Interval = value;
            }
        }

        public TrashCompactor()
        {
            m_Trashables = new List<ITrashable>();
        }

        // Update and cull if needed
        public void Update(GameTime gameTime)
        {
            // Update elapsed time
            m_Milliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            // If we should clean?
            if (m_Milliseconds > m_Interval)
            {
                // Clear out our timer
                m_Milliseconds = 0;
                // Then, for each trash object
                foreach (ITrashable trash in m_Trashables)
                {
                    // If it hasn't chimed in
                    if (!trash.Chimed)
                        // Clean it
                        trash.Clean(trash);
                }
            }
        }

        /// <summary>
        /// Add a trash object
        /// </summary>
        /// <param name="trash">Trash object to add</param>
        public void AddTrash(ITrashable trash)
        {
            if (m_Trashables != null && trash != null)
                m_Trashables.Add(trash);
        }
    }
}

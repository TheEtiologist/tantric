using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tantric.Graphical.Scenes
{
    public enum FadeMode
    {
        In,
        Out
    }

    public sealed class FadeImage : Scene, IDisposable
    {
        // Scene to fade from
        private Scene m_Previous;
        private SpriteBatch m_Batch;
        private Texture2D m_FadeTo;
        private double m_FadeTime;
        private double m_FadeCounter;
        private double m_FadePercentage;
        private double m_FadeScalar = 1.0;
        private string m_ResourceName;
        private FadeMode m_Mode;

        /// <summary>
        /// Initialize our fade scene.
        /// </summary>
        /// <param name="gm">Device manager to draw to.</param>
        /// <param name="previous">previous scene to fade from</param>
        /// <param name="fadeTime">how many ms to take</param>
        public FadeImage(GraphicsDeviceManager gm,
                    ContentManager content,
                    Scene previous, 
                    double fadeTime, 
                    string fadeToImage, 
                    FadeMode mode,
                    String name) : base(gm, content, name)
        {
            m_Previous = previous;
            m_FadeTime = fadeTime;
            m_ResourceName = fadeToImage;
            m_Mode = mode;
        }

        protected override void InternallyLoadResources()
        {
            m_Batch = new SpriteBatch(Graphics.GraphicsDevice);
            m_FadeTo = Content.Load<Texture2D>(m_ResourceName);
            // Make sure the thing we're fading from or to has resources loaded
            m_Previous.LoadResources();
        }

        protected override void InternallyUnloadResources()
        {
        }

        public override void Pause()
        {
            m_FadeScalar = 0.0;
        }

        public override void Unpause()
        {
            m_FadeScalar = 1.0;
        }

        public override void Logic(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Chime();
            m_FadeCounter += gameTime.ElapsedGameTime.TotalMilliseconds * m_FadeScalar;
            m_FadePercentage = m_FadeCounter / m_FadeTime;
            if (m_FadePercentage >= 1)
                Done = true;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            m_Previous.Draw(gameTime);

            m_Batch.Begin();
            if( m_Mode == FadeMode.In )
                m_Batch.Draw(m_FadeTo, new Vector2(0, 0), new Color(1f, 1f, 1f, (float)m_FadePercentage));
            else
                m_Batch.Draw(m_FadeTo, new Vector2(0, 0), new Color(1f, 1f, 1f, 1f - (float)m_FadePercentage));
            m_Batch.End();
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        #region IDisposable Members

        public void Dispose()
        {
            m_Batch.Dispose();
        }

        #endregion
    }
}

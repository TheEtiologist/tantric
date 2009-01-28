using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Tantric.World;

namespace Tantric.Graphical.Scenes
{
    sealed public class WorldScene : Scene, IDisposable
    {
        /// <summary>
        /// Time scalar.
        /// </summary>
        private double m_TimeScalar;
        private Dictionary<String, WorldLayer> m_Layers;
        private SpriteBatch m_Batch;
        private Camera m_Camera;

        public List<WorldLayer> Layers
        {
            get
            {
                return new List<WorldLayer>(m_Layers.Values);
            }
        }

        public Camera Camera
        {
            get
            {
                return m_Camera;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="content"></param>
        public WorldScene(GraphicsDeviceManager manager, ContentManager content, String name) : base(manager, content, name)
        {
            m_Layers = new Dictionary<String,WorldLayer>();
            m_Camera = new Camera();
            Engine.Instance.GameplayConductor.Prep(this);
        }

        /// <summary>
        /// Load our resources
        /// </summary>
        protected override void InternallyLoadResources()
        {
            if (!Loaded)
            {
                m_Batch = new SpriteBatch(Graphics.GraphicsDevice);
                //// Default:
                //// Background layer
                //// TODO: Replace with script loading.
                //m_Layers.Add("Background", new WorldLayer("Background"));
                //m_Layers["Background"].AddObject(new World.Objects.BackgroundObj("Background"));
                //m_Layers["Background"]["Background"].Scale(new Vector2(20, 20));
                //m_Layers["Background"].Layer = 5;
                //// Objects Layer
                //m_Layers.Add("Objects", new WorldLayer("Objects"));
                //m_Layers["Objects"].AddDriver(new ReplicatorDriver(m_Layers["Objects"]));
                ////m_Layers["Objects"].AddObject(new World.Objects.BackgroundObj("Object"));
                ////m_Layers["Objects"]["Background"].Translate(new Vector2(500, 100));
                ////m_Layers["Objects"]["Background"].SetScale(new Vector2(.5f, .5f));
                //World.Objects.InfluencePoint ip = new Replicators.World.Objects.InfluencePoint("Moveable0");
                //ip.Translate(new Vector2(.6f, .6f));
                //ip.Influence = 0.0f;
                //m_Layers["Objects"].AddObject(ip);
                //ip = new Replicators.World.Objects.InfluencePoint("Moveable1");
                //ip.Translate(new Vector2(.6f, .6f));
                //ip.Influence = -0.02f;
                //m_Layers["Objects"].AddObject(ip);
                //ip = new Replicators.World.Objects.InfluencePoint("Moveable2");
                //ip.Translate(new Vector2(.3f, .3f));
                //ip.Influence = 1.0f;
                //m_Layers["Objects"].AddObject(ip);
                //m_Layers["Objects"].Layer = 1;

                foreach (WorldLayer lay in m_Layers.Values)
                {
                    lay.LoadResources(Content);
                }
            }
        }

        public void AddLayer(WorldLayer lay)
        {
            if( lay != null && m_Layers != null )
            m_Layers.Add(lay.LayerName, lay);
        }

        public WorldLayer GetLayer(String name)
        {
            if (name != null && m_Layers != null)
                if (m_Layers.ContainsKey(name))
                    return m_Layers[name];
            return null;
        }

        /// <summary>
        /// Unload our resources
        /// </summary>
        protected override void InternallyUnloadResources()
        {
        }

        /// <summary>
        /// Pause...
        /// </summary>
        public override void Pause()
        {
            m_TimeScalar = 0.0;
        }

        /// <summary>
        /// Unpause...
        /// </summary>
        public override void Unpause()
        {
            m_TimeScalar = 1.0;
        }

        /// <summary>
        /// Update our game world.
        /// </summary>
        /// <param name="gt"></param>
        public override void Logic(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Chime();
            Engine.Instance.GameplayConductor.Evaluate(gameTime);
            foreach (WorldLayer layer in m_Layers.Values)
            {
                layer.Update((int)(gameTime.ElapsedGameTime.TotalMilliseconds * m_TimeScalar));
            }
        }

        /// <summary>
        /// Draw our game world.
        /// </summary>
        /// <param name="gt"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.Black);
            if (m_Batch != null)
            {
                m_Batch.Begin();
                foreach (WorldLayer layer in m_Layers.Values)
                {
                    layer.Draw(new GameTime(gameTime.TotalRealTime, gameTime.ElapsedRealTime, gameTime.TotalGameTime, new TimeSpan(0, 0, 0, 0, (int)(gameTime.ElapsedGameTime.TotalMilliseconds * m_TimeScalar))), m_Batch, m_Camera);
                }
                m_Batch.End();
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in keys)
                Engine.Instance.GameplayConductor.ProcessInput(Input.InputMap.InterpretKey(key), Vector2.Zero);
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed)
                Engine.Instance.GameplayConductor.ProcessInput(Input.InputMap.InterpretMouse(1), new Vector2(state.X, state.Y));
            if (state.RightButton == ButtonState.Pressed)
                Engine.Instance.GameplayConductor.ProcessInput(Input.InputMap.InterpretMouse(2), new Vector2(state.X, state.Y));
                
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            //    m_Camera.Translate(new Vector2(0, -CAMERA_SPEED));
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            //    m_Camera.Translate(new Vector2(0,  CAMERA_SPEED));
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            //    m_Camera.Translate(new Vector2(-CAMERA_SPEED, 0));
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            //    m_Camera.Translate(new Vector2( CAMERA_SPEED, 0));
            //if( Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            //    m_Layers["Objects"]["Moveable" + m_CurrentlySelected % 3].Translate(new Vector2(0, -(float)CAMERA_SPEED / 500));
            //if( Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            //    m_Layers["Objects"]["Moveable" + m_CurrentlySelected % 3].Translate(new Vector2(0, (float)CAMERA_SPEED / 500));
            //if( Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            //    m_Layers["Objects"]["Moveable" + m_CurrentlySelected % 3].Translate(new Vector2(-(float)CAMERA_SPEED / 500, 0));
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
            //    m_Layers["Objects"]["Moveable" + m_CurrentlySelected % 3].Translate(new Vector2((float)CAMERA_SPEED / 500, 0));
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.OemPlus))
            //    ((Replicators.World.Objects.InfluencePoint)m_Layers["Objects"]["Moveable" + m_CurrentlySelected % 3]).Influence += 0.005;
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.OemMinus))
            //    ((Replicators.World.Objects.InfluencePoint)m_Layers["Objects"]["Moveable" + m_CurrentlySelected % 3]).Influence -= 0.005;
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.R))
            //{
            //    m_Layers["Objects"]["Moveable0"].SetPosition(new Vector2(.6f, .6f));
            //    m_Layers["Objects"]["Moveable1"].SetPosition(new Vector2(.6f, .6f));
            //    m_Layers["Objects"]["Moveable2"].SetPosition(new Vector2(.3f, .3f));
            //    ((Replicators.World.Objects.InfluencePoint)m_Layers["Objects"]["Moveable0"]).Influence = 0.0f;
            //    ((Replicators.World.Objects.InfluencePoint)m_Layers["Objects"]["Moveable1"]).Influence = -0.02f;
            //    ((Replicators.World.Objects.InfluencePoint)m_Layers["Objects"]["Moveable2"]).Influence = 1.0f;
            //}
            //if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && m_SpaceDown == false)
            //{
            //    m_CurrentlySelected++;
            //    m_SpaceDown = true;
            //}
            //else if (!Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            //    m_SpaceDown = false;
        }

        #region IDisposable Members

        public void Dispose()
        {
            m_Batch.Dispose();
        }

        #endregion
    }
}

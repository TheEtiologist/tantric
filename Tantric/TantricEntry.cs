using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Tantric
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_Graphics;
        Graphical.SceneManager m_SceneManager;
        Trash.TrashCompactor m_TrashCompactor;
        Scripting.CommandInterpreter m_Interpreter;

        IGame m_Game;

        public IGame Game
        {
            get
            {
                return m_Game;
            }
        }

        public Graphical.SceneManager SceneManager
        {
            get
            {
                return m_SceneManager;
            }
        }

        public Trash.TrashCompactor TrashCompactor
        {
            get
            {
                return m_TrashCompactor;
            }
        }

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return m_Graphics;
            }
        }

        public World.IObjectFactory ObjectFactory
        {
            get
            {
                return m_Game.ObjFactory;
            }
        }

        public Logic.GamePlayConductor GameplayConductor
        {
            get
            {
                return m_Game.GamePlayConductor;
            }
        }

        public Scripting.CommandInterpreter CommandInterpreter
        {
            get
            {
                return m_Interpreter;
            }
        }

        /// <summary>
        /// Singleton's, EEVIL!
        /// </summary>
        static Engine _internalReference;
        static Object _lock;

        public static Engine Instance
        {
            get
            {
               if( _internalReference != null)
                   lock (_lock)
                   {
                       if (_internalReference != null)
                           return _internalReference;
                   }
               throw new InvalidOperationException("No singleton reference.");
            }
        }

        public Engine(IGame game)
        {
            _internalReference = this;
            _lock = new Object();
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            m_SceneManager = new Graphical.SceneManager();
            m_TrashCompactor = new Tantric.Trash.TrashCompactor();
            m_Interpreter = new Tantric.Scripting.CommandInterpreter();
            m_Game = game;

            Scripting.QuantumLanguage.InitializeLanguage(m_Interpreter);
            m_Interpreter.Evaluate("EntryScript");

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Graphical.Scene World = new Graphical.Scenes.WorldScene(graphics, Content);
            //m_Manager.QueueScene(World);
            //m_Manager.Cut(new Graphical.Scenes.FadeImage(graphics, Content, World, 4000, "Black", Graphical.Scenes.FadeImage.FadeMode.Out));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            m_SceneManager.GainFocus();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            m_SceneManager.LoseFocus();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            m_SceneManager.Update(gameTime);
            m_TrashCompactor.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>74-3
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            m_SceneManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}

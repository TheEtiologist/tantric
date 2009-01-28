using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Tantric.Scripting
{
    /// <summary>
    /// Specific implementation of scripting commands
    /// </summary>
    public sealed partial class QuantumLanguage
    {
        private QuantumLanguage()
        { }

        public static void InitializeLanguage(CommandInterpreter interpreter)
        {
            // Register Commands
            interpreter.RegisterCommand(new Command("AddScene", 2, AddScene));
            interpreter.RegisterCommand(new Command("AddLayer", 3, AddLayer));
            interpreter.RegisterCommand(new Command("AddObject", 4, AddObject));
            ParseScript("entryScript.qsf", interpreter);
        }

        public static void ParseScript(String fileName, CommandInterpreter interpreter)
        {
            StreamReader file = new StreamReader(fileName);
            String line = file.ReadLine();
            String line2 = file.ReadLine();
            while (line2 != null)
            {
                if (line2.Contains("{"))
                    interpreter.RegisterCommand(new Script(interpreter, fileName, line));
                line = line2;
                line2 = file.ReadLine();
            }
        }

        // Command Callbacks
        /// <summary>
        /// Add a scene
        /// </summary>
        /// <param name="Arguments"></param>
        public static void AddScene(List<String> arguments)
        {
            switch (arguments[0])
            {
                case "BlankScene":
                    Engine.Instance.SceneManager.AddScene(new Graphical.Scenes.BlankScene(Engine.Instance.GraphicsDeviceManager, Engine.Instance.Content, arguments[1]));
                    break;
                case "WorldScene":
                    Engine.Instance.SceneManager.AddScene(new Graphical.Scenes.WorldScene(Engine.Instance.GraphicsDeviceManager, Engine.Instance.Content, arguments[1]));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Add a layer to a scene
        /// </summary>
        /// <param name="Arguments"></param>
        public static void AddLayer(List<String> arguments)
        {
            //First argument: Scene name
            Graphical.Scenes.WorldScene Scene = (Graphical.Scenes.WorldScene)Engine.Instance.SceneManager.GetScene(arguments[0]);
            if (Scene == null)
                throw new ArgumentException("No such Scene as " + arguments[0]);
            //Second argument: Layer name
            World.WorldLayer lay = new World.WorldLayer(arguments[1]);
            //Third argument: Depth
            lay.Depth = double.Parse(arguments[2], CultureInfo.InvariantCulture);
            Scene.AddLayer(lay);
        }

        public static void AddObject(List<String> arguments)
        {
            // First Argument: Scene name
            Graphical.Scenes.WorldScene scene = (Graphical.Scenes.WorldScene)Engine.Instance.SceneManager.GetScene(arguments[0].Split(".".ToCharArray())[0]);
            // Second argument: Layer name
            World.WorldLayer layer = scene.GetLayer(arguments[0].Split(".".ToCharArray())[1]);
            World.WorldObject obj;
            // Third argument: Type
            switch (arguments[1])
            {
                default:
                    // Unknown, ask our game to create it instead
                    obj = Engine.Instance.ObjectFactory.Create(arguments[1], arguments.GetRange(2, 2));
                    layer.AddObject(obj);
                    break;
            }
            if (arguments.Count >= 5)
            {
                obj.SetPosition(new Microsoft.Xna.Framework.Vector2(float.Parse(arguments[4], CultureInfo.InvariantCulture), float.Parse(arguments[5], CultureInfo.InvariantCulture)));
            }
        }
    }
}

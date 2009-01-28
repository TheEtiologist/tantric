using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Input
{
    public static class InputMap
    {
        // Customization options:
        // Swap left and right mouse
        // control what keyboard keys map to camera controls
        // Everything else: sent to currently selected unit, or the "overall controller"

        // Example Scenarios:
        // Right button pressed -> ask input map what to do? -> send "Left" command to overall controller -> sent to UI or to select unit
        // Left button pressed -> ask input map what to do? -> send "Right" to overall controller -> Sent to currently selected or nothing
        // keyboard is pressed -> ask input map what to do? -> send "Camera_Left" to camera, etc.
        // keyboard is pressed -> ask input map what to do? -> not camera? -> send key to overall controller -> don't know? -> send to currently selected -> or ignore

        private static Boolean m_ButtonsSwapped;
        private static Dictionary<Microsoft.Xna.Framework.Input.Keys, String> m_CameraControls = new Dictionary<Microsoft.Xna.Framework.Input.Keys,string>();

        static InputMap()
        {
            Engine.Instance.CommandInterpreter.RegisterCommand(new Scripting.Command("SwapButtons", 0, SwapButtons));
            m_CameraControls.Add(Microsoft.Xna.Framework.Input.Keys.Left, "Camera_Left");
            m_CameraControls.Add(Microsoft.Xna.Framework.Input.Keys.Right, "Camera_Right");
            m_CameraControls.Add(Microsoft.Xna.Framework.Input.Keys.Up, "Camera_Up");
            m_CameraControls.Add(Microsoft.Xna.Framework.Input.Keys.Down, "Camera_Down");
            // TODO: Add Script camera control settings
        }

        static public void SwapButtons(List<String> args)
        {
            m_ButtonsSwapped = !m_ButtonsSwapped;
        }

        public static String InterpretKey(Microsoft.Xna.Framework.Input.Keys key)
        {
            if (m_CameraControls.ContainsKey(key))
                return m_CameraControls[key];
            else
                return Enum.GetName(key.GetType(), key);
        }

        public static String InterpretMouse(int buttonNumber)
        {
            if (buttonNumber == 1 || buttonNumber == 2)
            {
                //              Button Number
                //                1  |  2
                //             +-----------
                // Swapped:  T |Right| Left
                //           F |Left |Right
                if (buttonNumber == 1 ^ m_ButtonsSwapped)
                    return "Mouse_Left";
                return "Mouse_Right";
            }
            else
                return "Button" + buttonNumber;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace JZ.INPUT
{
    public static class Utils
    {
        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Converts a string into its corresponding key codes</para>
        /// </summary>
        public static KeyCode[] KeyCodesFromString(string _string)
        {
            KeyCode[] keys = new KeyCode[_string.Length];
            for(int ii = 0; ii < keys.Length; ii++)
            {
                keys[ii] = (KeyCode)Enum.Parse(typeof(KeyCode), _string[ii].ToString());
            }

            return keys;
        }

        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Returns true the frame the last key in the combo is pressed</para>
        /// </summary>
        public static bool CheckKeyCombo(string _keyCombo)
        {
            KeyCode[] combo = KeyCodesFromString(_keyCombo);
            for(int ii = 0; ii < combo.Length - 1; ii++)
                if(!Input.GetKey(combo[ii])) return false;

            return Input.GetKeyDown(combo[combo.Length - 1]);
        }

        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Returns true if a controller button is pressed</para>
        /// </summary>
        public static bool AnyControllerButton()
        {
            if(Gamepad.current == null) return false;

            foreach(InputControl control in Gamepad.current.allControls)
            {
                if(!(control is ButtonControl)) continue;
                if(!control.IsPressed() || control.synthetic) continue;
                return true;
            }
            return false;
        }

        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Returns true if a controller or computer button is pressed</para>
        /// </summary>
        public static bool AnyKeyOrButton()
        {
            return Input.anyKey || AnyControllerButton();
        }
    }
}


using UnityEngine;

namespace Assets.HandShankAdapation.InputAdapation
{
    public static class Key
    {
        public static  KeyCode[] KeyCodes =
#if UNITY_EDITOR
        {
            KeyCode.A,KeyCode.S,KeyCode.D,KeyCode.W,KeyCode.Space,KeyCode.Escape
        };
#else
        {
            KeyCode.JoystickButton0, XiaomiOk,
            KeyCode.Escape,
            KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
            KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
        };
#endif
        public static KeyCode XiaomiOk = (KeyCode)10;
        public static KeyCode LeftArrow =
#if UNITY_EDITOR
            KeyCode.A;
#else
         KeyCode.LeftArrow;
#endif
        public static KeyCode RightArrow =
#if UNITY_EDITOR
            KeyCode.D;
#else
         KeyCode.RightArrow;
#endif

        public static KeyCode Entry =
#if UNITY_EDITOR
            KeyCode.Space;
#else
         KeyCode.JoystickButton0;
#endif

        public static KeyCode Esc = KeyCode.Escape;
    }
}
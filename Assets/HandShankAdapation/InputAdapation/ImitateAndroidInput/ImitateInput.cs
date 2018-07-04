using UnityEngine;

namespace Assets.HandShankAdapation.ImitateAndroidInput
{
    public class ImitateInput
    {
        #if ! UNITY_EDITOR
        protected  static  AndroidJavaObject ajo = new AndroidJavaObject("com.example.imitatetouchinput.ImitateInput");
       #endif

        //public AndroidJavaObject ajo;
        public static void ImitateDown(int x, int y)
        {

#if UNITY_EDITOR
            PcImitateInput.SetCursorPos(x, y);
            PcImitateInput.Mouse_event(MouseEventFlag.LeftDown|MouseEventFlag.Absolute,x,y,0,0);
#else
            ajo.Call("Down", x, y);
#endif

        }

        public static void ImitateMove(int x, int y)
        {

#if UNITY_EDITOR
            PcImitateInput.SetCursorPos(x, y);
            PcImitateInput.Mouse_event(MouseEventFlag.Move|MouseEventFlag.Absolute,x,y,0,0);
#else
            ajo.Call("Move", x, y);
#endif
        }

        public static void ImitateUp(int x, int y)
        {

#if UNITY_EDITOR
            PcImitateInput.SetCursorPos(x, y);
            PcImitateInput.Mouse_event(MouseEventFlag.LeftUp|MouseEventFlag.Absolute,x,y,0,0);
#else
            ajo.Call("Up", x, y);
#endif
        }
    }
}

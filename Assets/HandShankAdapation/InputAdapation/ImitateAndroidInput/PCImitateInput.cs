#if UNITY_EDITOR

using System.Runtime.InteropServices;

namespace Assets.HandShankAdapation.ImitateAndroidInput
{
    public static class PcImitateInput
    {
        /// <summary>
        /// 导入模拟键盘的方法
        /// </summary>
        /// <param name="bVk" >按键的虚拟键值</param>
        /// <param name= "bScan" >扫描码，一般不用设置，用0代替就行</param>
        /// <param name= "dwFlags" >选项标志：0：表示按下，1:表示按着2：表示松开</param>
        /// <param name= "dwExtraInfo">一般设置为0</param>
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void Keybd_event(byte bvk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32.dll",EntryPoint = "mouse_event")]
        public static extern void Mouse_event(MouseEventFlag flags, int dx, int dy, uint data, int extraInfo);

        [DllImport("User32")]  
        public static extern  bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

    }
}
#endif
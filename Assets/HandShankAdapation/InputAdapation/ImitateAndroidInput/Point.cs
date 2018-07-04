#if UNITY_EDITOR
using System.Runtime.InteropServices;

namespace Assets.HandShankAdapation.ImitateAndroidInput
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;
 
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
 
        public override string ToString()
        {
            return ("X : " + X + ", Y : " + Y);
        }
    }
}
#endif
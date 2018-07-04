using System;

namespace Assets.HandShankAdapation.InputHandle
{
    public class InputOP
    {
        public  int InputType { get;protected set; }
        public  DateTime InputTime { get;protected set; }
        public int InputState { get; protected set; }
        public InputOP(int type, DateTime time,int state)
        {
            this.InputType = type;
            this.InputTime = time;
            this.InputState = state;
        }
    }
}

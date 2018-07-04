using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HandShankAdapation.InputHandle
{
    public class AndroidTVInput:IGetInput<InputOP>
    {
        protected bool HoldDown = false;
        /// <summary>
        /// 小米·的ok键
        /// </summary>
        public static KeyCode xiaomiOK = (KeyCode)10;

        [SerializeField] private KeyCode[] listenKeyCodes =
#if UNITY_EDITOR
        {
            KeyCode.A,KeyCode.S,KeyCode.D,KeyCode.W,KeyCode.Space,KeyCode.Escape
        };
#else
        {
            KeyCode.JoystickButton0, xiaomiOK,
            KeyCode.Escape,
            KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
            KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
        };
    #endif

        protected int maxcount = 100;
        private LinkedList<InputOP> inputs = new LinkedList<InputOP>();
        private KeyCode[] ListtenKeyCodes
        {
            get
            {
                return listenKeyCodes;
            }
        }

        private bool isanyinput = false;
        public override bool IsAnyInput 
        {
            get { return isanyinput; }
            protected set { isanyinput = value; }
        }

        public override bool GetInput()
        {
            if (HoldDown)
            {
                for (int i = 0; i < ListtenKeyCodes.Length; ++i)
                {
                    if (Input.GetKeyUp(ListtenKeyCodes[i]))
                    {
                        AddKeyToInput(ListtenKeyCodes[i],(int)KeyState.Up);
                        HoldDown = false;
                        return true;
                    }
                }
            }
            else if (Input.anyKeyDown)
            {
                for (int i = 0; i < ListtenKeyCodes.Length; ++i)
                {
                    if (Input.GetKeyDown(ListtenKeyCodes[i]))
                    {
                        AddKeyToInput(ListtenKeyCodes[i],(int)KeyState.Down);
                        RemoveWithCount(maxcount);
                        HoldDown = true;
                        return true;
                    }
                }
            }
               
            return false;
            
        }

        protected void AddKeyToInput(KeyCode keycode, int state)
        {
            DateTime time = DateTime.Now;
            if(keycode == xiaomiOK)inputs.AddLast(new InputOP((int)KeyCode.JoystickButton0,time,state));
            else inputs.AddLast(new InputOP((int) keycode,time,state));
        }
        private void RemoveWithCount(int maxnum)
        {
            
            int removecount = InputCount - maxnum ;
            while (removecount > 0)
            {
                inputs.RemoveFirst();
                --removecount;
            }
        }

       

        public override int InputCount
        {
            get { return inputs.Count; }
        }

        public override void Clear()
        {
            inputs = new LinkedList<InputOP>();
        }

        public override void RemoveLast()
        {
            inputs.RemoveLast();
        }

        public override void RemoveFirst()
        {
            inputs.RemoveFirst();
        }

        public override InputOP Last
        {
            get { return inputs.Last.Value; }
        }

        public override InputOP First
        {
            get { return inputs.First.Value; }
        }

        public override void AddLast(InputOP node)
        {
            inputs.AddLast(node);
        }

        public override void AddFirst(InputOP node)
        {
            inputs.AddFirst(node);
        }

        public override IEnumerable<InputOP> GetValues()
        {
            var node = inputs.First;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        public override IEnumerable<InputOP> GetReverseValues()
        {
            var node = inputs.Last;
            while (node != null)
            {
                yield return node.Value;
                node = node.Previous;
            }
        }
    }
}
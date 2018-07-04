using System.Collections;
using System.Collections.Generic;
using Assets.HandShankAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.InputHandle;
using GDGeek;
using UnityEngine;

namespace Assets.HandShankAdapation.InputAdapation.ImitateAndroidInput
{
    public class ImitateInputManager : Singleton<ImitateInputManager>
    {
        protected Queue<KeyValuePair<KeyState,Vector2>> eventqueue = new Queue<KeyValuePair<KeyState, Vector2>>();

        public int length = 4;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(SendImitateInputEvent());
        }

       
        public void OnInputEvent(Vector2 begin, Vector2 end)
        {
            OnPressEvent(begin);
            if (begin != end)
            {
                float width = end.x - begin.x;
                float height = end.y - begin.y;
                for (int i = 1; i <= 4; i++)
                {
                    Vector2 move = new Vector2(width*i/length+begin.x,height*i/length+begin.y);
                    OnMoveEvent(move);
                }
                
            }
            OnUpEvent(end);
        }

        public void OnInputEvent(Vector2 begin, Vector2 end,  Vector2[] moves)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Down,begin));

            foreach (var move in moves)
            {
                eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Hold,move));
            }

            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Up,end));
        }

        public void OnClickEvent(Vector2 p)
        {
            OnPressEvent(p);
            OnUpEvent(p);
        }
        public void OnPressEvent(Vector2 begin)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Down,begin));
        }

        public void OnMoveEvent(Vector2 move)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Hold,move));
        }
        public void OnUpEvent(Vector2 up)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Up,up));
        }

        IEnumerator SendImitateInputEvent()
        {
            while (true)
            {
               SendEvent();
                yield return null;
            }
          
        }

        protected void SendEvent()
        {
            if (eventqueue.Count > 0)
            {
                var input = eventqueue.Dequeue();
                if (input.Key == KeyState.Down)
                {
                    ImitateInput.ImitateDown((int) input.Value.x, (int) input.Value.y);
                }
                else if (input.Key == KeyState.Hold)
                {
                    ImitateInput.ImitateMove((int)input.Value.x,(int)input.Value.y);
                }
                else if (input.Key == KeyState.Up)
                {
                    ImitateInput.ImitateUp((int) input.Value.x, (int) input.Value.y);
                }
            }
        }
    }
}

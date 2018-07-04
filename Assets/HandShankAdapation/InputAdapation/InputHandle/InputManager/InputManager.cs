using System.Collections.Generic;
using UnityEngine;

namespace Assets.HandShankAdapation.InputHandle.InputManager
{
    public class InputManager<T> : MonoBehaviour, IInputManager<T>
    {
        protected IGetInput<T> inputs;

        public int InputCount
        {
            get { return inputs.InputCount; }
        }

        protected virtual void Update()
        {
            if (inputs.GetInput())
            {
                isanyinput = true;
            }
            else
            {
                isanyinput = false;
            }
        }

        protected bool isanyinput = false;

        public bool IsAnyInput
        {
            get { return isanyinput; }
        }


        public T Used()
        {
            var last = inputs.Last;
            inputs.RemoveLast();
            return last;
        }

        public T ReverseUsed()
        {
            var first = inputs.First;
            inputs.RemoveFirst();
            return first;
        }

        public IEnumerable<T> UsedAll()
        {
            if (InputCount > 0) yield return Used();
        }

        public IEnumerable<T> ReverseUsedAll()
        {
            if (InputCount > 0) yield return ReverseUsed();
        }

        public T GetInput()
        {
            return inputs.Last;
        }

        public T GetReverseInput()
        {
            return inputs.First;
        }

        public IEnumerable<T> GetInputs()
        {
            foreach (var value in inputs.GetReverseValues())
            {
                yield return value;
            }
        }

        public IEnumerable<T> GetReverseInputs()
        {
            foreach (var varlue in inputs.GetValues())
            {
                yield return varlue;
            }
        }
    }
}
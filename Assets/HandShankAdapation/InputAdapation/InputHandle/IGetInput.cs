using System.Collections.Generic;

namespace Assets.HandShankAdapation.InputHandle
{
    public abstract class IGetInput<T>
    {
        public abstract bool IsAnyInput { get; protected set; }
        public abstract bool GetInput();
        public abstract int InputCount { get; }
        public abstract void Clear();
        public abstract void RemoveLast();
        public abstract void RemoveFirst();
        public abstract T Last { get; }
        public abstract T First { get; }
        public abstract void AddLast(T node);
        public abstract void AddFirst(T node);
        public abstract IEnumerable<T> GetValues();
        public abstract IEnumerable<T> GetReverseValues();
    }
}

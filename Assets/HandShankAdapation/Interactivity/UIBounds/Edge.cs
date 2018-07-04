using System;

namespace Assets.HandShankAdapation.UIBounds
{
    public abstract class Edge:IComparable<Edge>
    {
        public int Index;
       public abstract float Value { get; }
       public UIRect UIRect;

        public Edge(UIRect uirect)
        {
            this.UIRect = uirect;
        }

        public virtual int CompareTo(Edge other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}

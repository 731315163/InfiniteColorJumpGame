namespace Assets.HandShankAdapation.UIBounds
{
   public abstract class PreviousEdge:Edge
    {
        protected PreviousEdge(UIRect transform) : base(transform)
        {
        }

        public override int CompareTo(Edge other)
        {
            int res = base.CompareTo(other);
            if (res == 0 && other is NextEdge) return -1;
            return res;
        }
    }
}

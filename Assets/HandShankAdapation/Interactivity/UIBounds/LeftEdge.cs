namespace Assets.HandShankAdapation.UIBounds
{
    public class LeftEdge:PreviousEdge
    {
        

        public override float Value
        {
            get { return UIRect.Left; }
        }

        public LeftEdge(UIRect transform) : base(transform)
        {
        }
    }
}

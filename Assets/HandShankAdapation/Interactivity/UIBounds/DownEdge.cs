namespace Assets.HandShankAdapation.UIBounds
{
    public class DownEdge:PreviousEdge
    {
        public DownEdge(UIRect transform):base(transform)
        {
        }

        public override float Value
        {
            get { return UIRect.Down; }
        }
    }
}

namespace Assets.HandShankAdapation.UIBounds
{
    public class RightEdge:NextEdge
    {
        

        public override float Value
        {
            get { return UIRect.Right; }
        }

        public RightEdge(UIRect transform) : base(transform)
        {
        }
    }
}

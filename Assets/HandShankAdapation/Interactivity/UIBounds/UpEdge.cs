namespace Assets.HandShankAdapation.UIBounds
{
    public class UpEdge:NextEdge
    {
        private float _value;

        public override float Value
        {
            get { return UIRect.Up; }
        }

        public UpEdge(UIRect transform) : base(transform)
        {
        }
    }
}

using Assets.HandShankAdapation.UIBounds;
using UnityEngine;

namespace Assets.HandShankAdapation.UGUI
{
   public class CanvasUIRect:UGUIRect
    {
       

        public static int CompareCanvasOrder(UIRect a, UIRect b)
        {
            Canvas a1 = a.UIComponent as Canvas;
            Canvas b1 = b.UIComponent as Canvas;
            if (a1 == null && b1 == null) return 0;
            if (a1 == null) return 1;
            if (b1 == null) return -1;
            return a1.sortingOrder.CompareTo(b1.sortingOrder);
        }
        public CanvasUIRect(Behaviour go) : base(go)
        {
        }
    }
}

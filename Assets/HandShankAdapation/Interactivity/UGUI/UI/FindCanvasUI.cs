using Assets.HandShankAdapation.UIBounds;
using UnityEngine;

namespace Assets.HandShankAdapation.UGUI
{
    public class FindCanvasUI:IFindUIRect
    {
        public UIRect Find(Behaviour component)
        {
            var canvas = component as Canvas;
            if (canvas == null) return null;
            var canvasui = new CanvasUIRect(canvas){SortOrder =canvas.sortingOrder};
            return canvasui;
        }
    }
}

using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.HandShankAdapation.UGUI
{
   public class FindClickUI:IFindUIRect
    {
        public UIRect Find(Behaviour component)
        {
           
            var click = component as IPointerClickHandler;
            if (click != null)
            {
                return new ClickUIRect(component);
            }
            return null;
        }
    }
}

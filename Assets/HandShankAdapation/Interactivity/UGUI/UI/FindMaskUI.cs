using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HandShankAdapation.UGUI.UI
{
    public class FindMaskUI:IFindUIRect
    {
        public UIRect Find(Behaviour component)
        {
            var mask = component as Mask;
            if (mask != null)
            {
               // var image = component.GetComponent<Image>();
                return new MaskUIRect(component);
            }
            return null;
        }
    }
}

using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HandShankAdapation.UGUI
{
    class FindImageUI:IFindUIRect
    {
        public UIRect Find(Behaviour component)
        {
            var image = component as MaskableGraphic;
            if (image != null)
            {
                return new ImageUIRect(component);
            }
            return null;
        }
    }
}

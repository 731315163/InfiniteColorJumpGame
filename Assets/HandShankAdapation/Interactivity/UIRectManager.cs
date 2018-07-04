using Assets.HandShankAdapation.UIBounds;
using GDGeek;
using UnityEngine;

namespace Assets.HandShankAdapation
{
   public abstract class UIRectManager:Singleton<UIRectManager>
   {
       public abstract UIRect GetRoot();

       public abstract void SetRoot(Behaviour root);
       public abstract UIContainer FindActivityUIRects();
       public abstract UIContainer FindActivityUIRectsOnMask();

   }
}

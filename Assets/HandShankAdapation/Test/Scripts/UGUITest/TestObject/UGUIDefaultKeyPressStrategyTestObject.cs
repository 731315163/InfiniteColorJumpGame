#if UNITY_EDITOR
using System.Collections.Generic;
using Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy;
using Assets.HandShankAdapation.UGUI.AdapationStrategy;
using Assets.HandShankAdapation.UIBounds;

namespace Assets.HandShankAdapation.Test.Scripts.UGUITest.TestObject
{
    public class UGUIDefaultKeyPressStrategyTestObject : UGUIDefaultKeyPressStrategy
    {
        
        public UIRect GetUpUIRectTest(UIRect target,List<Edge>list)
        {
            return GetUpUIRect(target, list);
        }
        
        public UIRect GetDownUIRectTest(UIRect target,List<Edge>list)
        {
            return GetDownUIRect(target, list);
        }
        
        public UIRect GetLeftUIRectTest(UIRect target,List<Edge>list)
        {
            return GetLeftUIRect(target, list);
        }
        
        public UIRect GetRightUIRectTest(UIRect target,List<Edge>list)
        {
            return GetRightUIRect(target, list);
        }
    }
}
#endif
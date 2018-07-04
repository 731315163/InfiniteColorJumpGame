#if UNITY_EDITOR
using System.Collections.Generic;
using Assets.HandShankAdapation.Interactivity.UGUI;
using Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy;
using Assets.HandShankAdapation.Test.Scripts.UGUITest.TestObject;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;


namespace Assets.HandShankAdapation.Test.Scripts.UGUITest
{
    [RequireComponent(typeof(UGUIDefaultKeyPressStrategy))]
    public class UGUIDefaultKeyPressStrategyTest : MonoBehaviour
    {
        protected FindInteractivityUI Find;

        public UGUIDefaultKeyPressStrategyTestObject defaultStrategy;

        public Component Mid;

        public Component Up;

        public Component Down;

        public Component Left;

        public Component Right;

            // Use this for initialization

        void Start()
        {
            if (Find == null) Find = FindInteractivityUI.Instance as FindInteractivityUI;
            Invoke();
            
        }

        
        

        public void Invoke()
        {
            var container = Find.FindActivityUIRects();
            UIRect target = null;
            foreach (var edge in container.Row)
            {
                if (edge.UIRect.UIComponent == Mid)
                {
                    target = edge.UIRect;
                    break;
                }
            }
            ClickButtonTest(target);
            GetDownUIRectTest(target,container.Column);
            GetLeftUIRectTest(target,container.Row);
            GetRightUIRectTest(target,container.Row);
            GetUpUIRectTest(target,container.Column);
            Debug.Log("DefaulteyStrategy is Pass");
        }

        private bool clicktest = false;
        public void ClickTest()
        {
            clicktest = true;
            
        }
        public void ClickButtonTest(UIRect rect)
        {
            var button = rect.UIComponent as IPointerClickHandler;
            Assert.IsNotNull(button);
            var e = new PointerEventData(EventSystem.current);
            e.button = PointerEventData.InputButton.Left;
            button.OnPointerClick(e);
            Assert.IsTrue(clicktest);
        }

        public void GetUpUIRectTest(UIRect rect,List<Edge>col)
        {
            var ui = defaultStrategy.GetUpUIRectTest(rect,col);
            Assert.AreEqual(ui.UIComponent,Up);
        }

        public void GetDownUIRectTest(UIRect rc,List<Edge>col)
        {
            var ui = defaultStrategy.GetDownUIRectTest(rc,col);
            Assert.AreEqual(ui.UIComponent,Down);
        }

        public void GetLeftUIRectTest(UIRect rc,List<Edge> row)
        {
            var ui = defaultStrategy.GetLeftUIRectTest(rc,row);
            Assert.AreEqual(ui.UIComponent,Left);
        }

        public void GetRightUIRectTest(UIRect rc,List<Edge> row)
        {
            var ui = defaultStrategy.GetRightUIRectTest(rc,row);
            Assert.AreEqual(ui.UIComponent,Right);
        }
    }
}
#endif
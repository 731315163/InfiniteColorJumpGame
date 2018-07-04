#if UNITY_EDITOR
using Assets.HandShankAdapation.Interactivity.UGUI;
using Assets.HandShankAdapation.UGUI.UI;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets.HandShankAdapation.Test.Scripts.UGUITest
{

    [RequireComponent(typeof(FindInteractivityUI))]
    public class FindInteractivityUITest : MonoBehaviour {
        protected FindInteractivityUI FindInteractivityUI;

        public Canvas RootCanvas;
        // Use this for initialization
        void Start() {
            FindInteractivityUI = GetComponent<FindInteractivityUI>();

            Invoke();
        }

        public void Invoke() {
            FindRootCanvasTest();
            FindUIRectTest();
            FindActivityUITest();
            FindActivityUIRectsOnMaskTest();
            FindActivityUIRectsOnMaskWithMaskUI();
            Debug.Log("FindInteractivityUI is Pass");
        }

        public void FindUIRectTest() {
            var go = new GameObject("FindUIRectTest");
            go.AddComponent<Image>();
            var com = go.AddComponent<Mask>();
            UIRect uirect = FindInteractivityUI.FindUIRect(com.transform);
            Assert.AreEqual(typeof(MaskUIRect), uirect.GetType());
        }
        public void FindRootCanvasTest() {
            var canvas = FindInteractivityUI.GetRoot();
            Assert.AreEqual(canvas.UIComponent, RootCanvas);
        }

        public void FindActivityUITest() {
            var uicontainer = FindInteractivityUI.FindActivityUIRects();
            var ui = uicontainer.Row[0].UIRect.UIComponent;
            Assert.AreEqual(ui.name, "Text");
        }

        public void FindActivityUIRectsOnMaskTest() {
            var uicontainer = FindInteractivityUI.FindActivityUIRectsOnMask();
            foreach (var edge in uicontainer.Row) {
                Assert.AreNotEqual(edge.UIRect.UIComponent.name, "Button2");
            }
        }

        public void FindActivityUIRectsOnMaskWithMaskUI() {
            var uicontainer = FindInteractivityUI.FindActivityUIRectsOnMask();
            bool flag = false;
            foreach (var edge in uicontainer.Row) {
                if (edge.UIRect.UIComponent.name == "Mask") flag = true;
                Assert.AreNotEqual(edge.UIRect.UIComponent.name, "Mask2");
            }
            Assert.IsTrue(flag);
        }

    }
}
#endif
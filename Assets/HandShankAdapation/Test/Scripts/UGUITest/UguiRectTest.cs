#if UNITY_EDITOR
using Assets.HandShankAdapation.UGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HandShankAdapation.Test.Scripts.UGUITest
{
    [RequireComponent(typeof(RectTransform))]
    public class UguiRectTest : MonoBehaviour
    {
        protected UGUIRect uirect;

        public Image image;
        // Use this for initialization
        void Start () {
		    uirect = new UGUIRect(this);
            LeftDownTest();
           // RightUpTest();
        }

        public void LeftDownTest()
        {
            image.transform.position = new Vector2(uirect.Left,uirect.Down);
        }

        public void RightUpTest()
        {
            image.transform.position = new Vector2(uirect.Right,uirect.Up);
        }
      
    }
}
#endif
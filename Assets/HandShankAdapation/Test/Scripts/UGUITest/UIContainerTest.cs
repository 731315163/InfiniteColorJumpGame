using Assets.HandShankAdapation.UGUI;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets.HandShankAdapation.Test.Scripts.UGUITest
{
    public class UIContainerTest : MonoBehaviour
    {
        protected UIContainer container;

        public Image image;
        // Use this for initialization
        void Start()
        {
            container = new UIContainer();
            var uirect = new UGUIRect(image);
            container.Insert(uirect);

            Invoke();
        }

        public void Invoke()
        {
            InsertTest();
            SortTest();
            Debug.Log(" UIContainer is Pass");
        
        }
        public void InsertTest()
        {
            Assert.AreEqual(container.Count,1);

        }

        public void SortTest()
        {
            container.Sort();
            for (int i = 0; i < container.Row.Count - 1; i++)
            {
                if(container.Row[i].Value > container.Row[i+1].Value)Assert.IsTrue(false);
                if(container.Column[i].Value > container.Column[i+1].Value)Assert.IsTrue(false);

            }
        }
    
    }
}

#if UNITY_EDITOR
using Assets.HandShankAdapation.UGUI;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.HandShankAdapation.Test.Scripts.UGUITest
{
    [RequireComponent(typeof(Canvas))]
    public class FindCanvasTest : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            var finder = new FindCanvasUI();
            UIRect ui = null;
            var components = this.GetComponents<Behaviour>();
            foreach (var component in components)
            {
                ui = ui ?? finder.Find(component);
            }
            Assert.IsNotNull(ui);
            Debug.Log("FindCanvas is Pass");
        }
    }
}
#endif
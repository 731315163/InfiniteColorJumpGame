#if UNITY_EDITOR
using Assets.HandShankAdapation.UGUI;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets.HandShankAdapation.Test.Scripts.UGUITest
{
    [RequireComponent(typeof(Button))]
    public class FindClickUITest : MonoBehaviour
    {
        
        // Use this for initialization
        void Start()
        {
            var finder = new FindClickUI();
            UIRect ui = null;
            var components = this.GetComponents<Behaviour>();
            foreach (var component in components)
            {
                ui = ui ?? finder.Find(component);
            }
            Assert.IsNotNull(ui);
            Debug.Log("FindClickUITest is Pass");
        }

        
    }
}
#endif
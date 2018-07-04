using Assets.HandShankAdapation.UGUI;
using UnityEngine;

namespace Assets.HandShankAdapation.Interactivity.UGUI
{

    [CreateAssetMenu(menuName = "MySubMenue/Create MyScriptableObject ")]
    public class Finder:ScriptableObject
    {
        public IFindUIRect[] Finders;
        
    }
}

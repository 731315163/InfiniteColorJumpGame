using UnityEditor;
using UnityEngine;

namespace Assets.HandShankAdapation.Editor
{
    public class DelePrefabs {

        [MenuItem("Tools/Prefabs/Dele")]
        public static void DeleAllPlaterPrefabs()
        {
            PlayerPrefs.DeleteAll();
        }
        [MenuItem("Tools/Prefabs/SetPrefabFloat")]
        public static void SetPrefabFloat()
        {
            PlayerPrefs.SetFloat("Money",100f);
        }
    }
}

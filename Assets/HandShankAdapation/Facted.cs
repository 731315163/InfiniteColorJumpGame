using UnityEngine;

namespace Assets.HandShankAdapation
{
    /// <summary>
    /// 请将此脚本顺序设为负值
    /// </summary>
    public class Facted : MonoBehaviour
    {
        private static Facted instance;
        public static Facted Instance
        {
            get { return instance; }
        }

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        public static T Get<T>() where T : class 
        {
            var components = Instance.GetComponents<Component>();
            foreach (var component in components)
            {
                var c = component as T;
                if (c != null) return c;
            }
            return default(T);
        }
    }
}

using GDGeek;
using UnityEngine;

namespace Assets.HandShankAdapation
{
    public class RealTime : Singleton<RealTime>
    {
        protected float LastTime;
        public float deltaTime { get; protected set; }
        // Use this for initialization
        void Start()
        {
            LastTime = Time.realtimeSinceStartup;
        }

        // Update is called once per frame
        void Update()
        {
            deltaTime = Time.realtimeSinceStartup - LastTime;
            LastTime = Time.realtimeSinceStartup;
        }
    }
}

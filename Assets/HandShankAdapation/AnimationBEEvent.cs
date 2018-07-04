using Assets.HandShankAdapation.ImitateAndroidInput;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.HandShankAdapation
{
    public class AnimationBEEvent : MonoBehaviour
    {

        public Transform target;
        public UnityAction Begin;
        public UnityAction End;
        public void PlayBegin()
        {
            if (Begin != null) Begin();
        }

        public void PlayEnd()
        {
            if (End != null) End();
        }
    }
}

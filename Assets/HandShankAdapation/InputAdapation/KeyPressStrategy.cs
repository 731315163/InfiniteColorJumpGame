using System.Linq;
using Assets.HandShankAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.InputAdapation.ImitateAndroidInput;
using GDGeek;
using UniRx.Operators;
using UnityEngine;

namespace Assets.HandShankAdapation.InputAdapation
{
    public abstract class KeyPressStrategy : MonoBehaviour
    {
        public string Name;

        public virtual StrategyState StrategyInit()
        {
            var state = new StrategyState(this);
            state.onStart += OnStart;
            state.onUpdate += OnUpdate;
            state.onOver += OnOver;
            return state;
        }
        public virtual void OnStart(){}
        public virtual void OnUpdate(float t){}

        public virtual void OnOver(){}
        public virtual void ClickTarget(Vector3 worldpositon,int layer)
        {
            Vector3 p = CameraManager.Instance.WorldPositionToScreenPosition(worldpositon, layer);
            ImitateInputManager.Instance.OnClickEvent(p);
        }

        public Transform FindGameObject(string name)
        {
            GameObject[] goes = Object.FindObjectsOfType<GameObject>();
            foreach (var go in goes)
            {
                if (go.name == name) return go.transform;
            }
            return null;
        }
       
    }

    public class StrategyState : UpateState
    {
        public KeyPressStrategy Strategy;

        public StrategyState(KeyPressStrategy strategy)
        {
            this.Strategy = strategy;
        }
    }
  
   
}

using Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy;
using Assets.HandShankAdapation.Messenger;
using UnityEngine;

namespace Assets.HandShankAdapation.CodeModel
{
    public class ChangeState : MonoBehaviour
    {

        protected void Change(string name)
        {
            MessageManager.Instance.Broadcast(Strategy.ChangeStrategy,name);
        }
      
    }
}

using Assets.HandShankAdapation.Messenger;
using Assets.HandShankAdapation.UGUI.AdapationStrategy;

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

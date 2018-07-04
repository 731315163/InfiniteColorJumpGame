using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using Assets.HandShankAdapation.Messenger;
using UnityEngine;

namespace Assets.HandShankAdapation.InputAdapation.InputHandle.InputManager
{
    public class TvInputManager:InputManager<InputOP>
    {
         void Awake()
        {
            this.inputs = new AndroidTVInput();
        }
        
        protected override void  Update()
        {
            base.Update();
            if (IsAnyInput)
            {
                var key = ((KeyCode) GetInput().InputType).ToString();
                MessageManager.Instance.Broadcast<IInputManager<InputOP>>(key,this,MessengerMode.REQUIRE_LISTENER);
            }

        }
    }
}

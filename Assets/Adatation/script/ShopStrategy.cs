using Assets.HandShankAdapation.InputAdapation;
using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using Assets.HandShankAdapation.Interactivity.UGUI;
using Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Adatation.script
{
    public class ShopStrategy : UGUINoPointerStrategy {

        public override void PointerMoveToUIRect(UIRect target,Direction direction)
        {
            if(target == null)
                return;
            if (PointerAttachUI != null)
                PointerAttachUI.UIComponent.GetComponent<SelectItem>().NotSelect();
        
            PointerAttachUI = target;
            PointerAttachUI.UIComponent.GetComponent<SelectItem>().Select();
        }

        public Canvas RootCanvas;
        public UnityEvent ExitEvent;
        public override void EscOperate(IInputManager<InputOP> inputmanager)
        {
            if (inputmanager.GetInput().InputState != (int)KeyState.Down)
                return;
            if (PointerAttachUI != null)
                PointerAttachUI.UIComponent.GetComponent<SelectItem>().NotSelect();
            ExitEvent.Invoke();
            (FindInteractivityUI.Instance as FindInteractivityUI).SetRoot(RootCanvas);
        }
    }
}

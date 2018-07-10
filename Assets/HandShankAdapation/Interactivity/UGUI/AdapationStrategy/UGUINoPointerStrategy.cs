using System.Collections;
using Assets.HandShankAdapation.InputAdapation;
using Assets.HandShankAdapation.InputAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;

namespace Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy
{
    public class UGUINoPointerStrategy:BaseUGUIStrategy,ILeftArrowOperate,IEscOperate,IEnterOperate,IRigthtArrowOperate,IDownArrowOperate,IUpArrowOperate
    {
        public UIRect PointerAttachUI;
  
        public override StrategyState StrategyInit()
        {
            var state = base.StrategyInit();
            state.onStart += StartState;
            state.onOver += EndState;
            state.onUpdate += UpdateState;
            return state;
        }

        protected virtual void StartState()
        {
            CanvasRoot = UIRectManager.Instance.GetRoot();
            PointerAttachUI = null;
        }

    
        protected virtual void UpdateState(float t)
        {
            
            if (CanvasRoot == null)
            {
                CanvasRoot = UIRectManager.Instance.GetRoot();
            }
            else if (PointerAttachUI == null || ! PointerAttachUI.IsActivite || ! IsContainUIRect(container, PointerAttachUI))
            {
                InitPointerLocation();
            }
        }

        protected virtual void EndState()
        {
            CanvasRoot = null;
            PointerAttachUI = null;
        }
     
      

        public IEnumerator ResetPointerAttachUI()
        {
            yield return new WaitForSecondsRealtime(Delay);
           ResetPointerAttackUI();
        }

        public void ResetPointerAttackUI()
        {
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            if (PointerAttachUI == null || ! PointerAttachUI.IsActivite  || ! IsContainUIRect(container, PointerAttachUI))
                 PointerAttachUI = null;
        }
        
        
      
        public bool InitPointerWithRow = false;

        public virtual void InitPointerLocation()
        {
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            if (PointerAttachUI != null && IsContainUIRect(container,PointerAttachUI))
                return;
            PointerAttachUI = InitPointerWithRow ?FindInitUIRect(container.Row)  : FindInitUIRect(container.Column);
            PointerMoveToUIRect(PointerAttachUI,Direction.Down);
        }

      
       

        public override void ClickTarget(Vector3 worldpositon, int layer)
        {
            var canvas = UIRectManager.Instance.GetRoot().UIComponent as Canvas;
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                Vector3 p = CameraManager.Instance.WorldPositionToScreenPositionWithScreenOverlay(worldpositon);
                ImitateInputManager.Instance.OnClickEvent(p);
            }
            else
            {
                base.ClickTarget(worldpositon, layer);
            }
        }
        public virtual void EnterOperate(IInputManager<InputOP> inputmanager)
        {
            if (inputmanager.GetInput().InputState !=(int)KeyState.Down)
                return;
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            
            SendUGUIClickEvent(PointerAttachUI);

            StartCoroutine(ResetPointerAttachUI());
        }
        public virtual void RightArrowOperate(IInputManager<InputOP> inputmanager)
        {
           
            if(inputmanager.GetInput().InputState !=(int)KeyState.Down)return;
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            var uirect = GetRightUIRect(PointerAttachUI,container.Row);
            
            PointerMoveToUIRect(uirect,Direction.Right);
        }
        public virtual void DownArrowOperate(IInputManager<InputOP> inputmanager)
        {
            if(inputmanager.GetInput().InputState !=(int)KeyState.Down)return;
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            
            var uirect = GetDownUIRect(PointerAttachUI,container.Column);
            PointerMoveToUIRect(uirect,Direction.Down);
        }

        public virtual void UpArrowOperate(IInputManager<InputOP> inputmanager)
        {
            if(inputmanager.GetInput().InputState !=(int)KeyState.Down)return;
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            container.Sort();
            var uirect = GetUpUIRect(PointerAttachUI,container.Column);
            
          
            PointerMoveToUIRect(uirect,Direction.Up);
        }
       

        public virtual void LeftArrowOperate(IInputManager<InputOP> inputmanager)
        {
            if(inputmanager.GetInput().InputState !=(int)KeyState.Down)
                return;
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
           
            var uirect = GetLeftUIRect(PointerAttachUI,container.Row);
            PointerMoveToUIRect(uirect,Direction.Left);
        }

       
        public virtual void EscOperate(IInputManager<InputOP> inputmanager)
        {
            if (inputmanager.GetInput().InputState != (int) KeyState.Down) return;
            ClickEscKey();
            StartCoroutine(ResetPointerAttachUI());
        }

        protected void ClickEscKey()
        {
            if(CloseUINames == null)return;
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            UIRect com = null;
            foreach (var edge in container.Row)
            {
                if (edge is PreviousEdge && IsCloseButton(edge.UIRect))
                {
                    com = edge.UIRect;
                }
            }
            SendUGUIClickEvent(com);
        }

     
        public virtual void PointerMoveToUIRect(UIRect target,Direction direction)
        {
          
        }
     
    }
}
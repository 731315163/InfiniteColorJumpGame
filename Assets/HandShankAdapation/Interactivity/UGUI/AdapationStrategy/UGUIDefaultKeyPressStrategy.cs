using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HandShankAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.InputAdapation;
using Assets.HandShankAdapation.InputAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using Assets.HandShankAdapation.UGUI;
using Assets.HandShankAdapation.UIBounds;
using GDGeek.Pro;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;


namespace Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy
{
    public class UGUIDefaultKeyPressStrategy : KeyPressStrategy,ILeftArrowOperate,IEscOperate,IEnterOperate,IRigthtArrowOperate,IDownArrowOperate,IUpArrowOperate
    {
        public GameObject PointerPre;
        public GameObject Pointer;
        public UIRect PointerAttachUI;
        public string[] CloseUINames = new string[0];
        public string[] PrioritySelectUINames = new string[0];
        protected UIContainer container;
        protected UIRect CanvasRoot;

        public float Delay = 0.25f;
        // Use this for initialization
        void Start()
        {
            InitCloseUINames();
        }

        void OnDestroy()
        {
            Destroy(Pointer);
        }

        public override StrategyState StrategyInit()
        {
            var state = new StrategyState(this);
            state.onStart += StartState;
            state.onOver += EndState;
            state.onUpdate += UpdateState;
            return state;
        }

        protected void StartState()
        {
            CanvasRoot = UIRectManager.Instance.GetRoot();
            if (CanvasRoot != null && PointerPre != null)
            {
                Pointer = Instantiate(PointerPre, CanvasRoot.UIComponent.transform);
                Pointer.show();
            }
            PointerAttachUI = null;
        }

    
        protected void UpdateState(float t)
        {
            
            if (CanvasRoot == null)
            {
                CanvasRoot = UIRectManager.Instance.GetRoot();
                if (CanvasRoot != null && Pointer == null) Pointer = Instantiate(Pointer, CanvasRoot.UIComponent.transform);
            }
            else if (PointerAttachUI == null || ! PointerAttachUI.IsActivite || ! IsContainUIRect(container, PointerAttachUI))
            {
                InitPointerLocation();
            }
            else if (Pointer.transform.position != PointerAttachUI.Position)
            {
                Pointer.transform.position = PointerAttachUI.Position;
            }
        }

        protected void EndState()
        {
            Destroy(Pointer);
            CanvasRoot = null;
            PointerAttachUI = null;
        }
        protected void InitCloseUINames()
        {
            CloseUINames = InitUINamesWithClone(CloseUINames);
            PrioritySelectUINames = InitUINamesWithClone(PrioritySelectUINames);
        }
    
        protected string[] InitUINamesWithClone(string[] array)
        {
            string[] uinames = new string[array.Length * 2];
            for (var i = 0; i < uinames.Length; i++)
            {
                if (i < array.Length)
                {
                    uinames[i] = array[i];
                }
                else
                {
                    uinames[i] = array[ i - array.Length]+"(Clone)";
                }
            }
            return uinames;
        }


        protected bool IsPrioritySelectUI(string name)
        {
            for (var i = 0; i < PrioritySelectUINames.Length; i++)
            {
                if (PrioritySelectUINames[i] == name) return true;
            }
            return false;
        }
        public virtual void EnterOperate(IInputManager<InputOP> inputmanager)
        {
            if (inputmanager.GetInput().InputState !=(int)KeyState.Down)
                return;
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            
            SendUGUIClickEvent(PointerAttachUI);

            StartCoroutine(ResetPointerAttachUI());
        }

        public IEnumerator ResetPointerAttachUI()
        {
            yield return new WaitForSecondsRealtime(Delay);
           ResetPointerAttackUI();
        }

        public void ResetPointerAttackUI()
        {
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            if (PointerAttachUI == null || ! PointerAttachUI.IsActivite || PointerAttachUI.Position != Pointer.transform.position || ! IsContainUIRect(container, PointerAttachUI))
            {
                PointerAttachUI = null;
            }
        }
        
        
        protected void LogContainer(bool isrow = false)
        {
            List<Edge> lists = isrow ? container.Row : container.Column;
            foreach (var edge in lists)
            {
                if (edge is PreviousEdge)
                {
                    Debug.Log(edge.UIRect.UIComponent.name,edge.UIRect.UIComponent.gameObject);
                }
            }
        }
        
        protected UIRect LogContainer(string name)
        {
            foreach (var edge in container.Row)
            {
                if (edge is PreviousEdge && edge.UIRect.UIComponent.name == name)
                {
                    return edge.UIRect;
                }
            }

            return null;
        }
        public bool InitPointerWithRow = false;

        public virtual void InitPointerLocation()
        {
            container = UIRectManager.Instance.FindActivityUIRectsOnMask();
            if (PointerAttachUI != null && Pointer.transform.position == PointerAttachUI.Position && IsContainUIRect(container,PointerAttachUI))return;
            PointerAttachUI = InitPointerWithRow ?FindInitUIRectWithRow()  : FindInitUIRectWithColume();
            PointerMoveToUIRect(PointerAttachUI,Direction.Down);
        }

        protected UIRect FindInitUIRectWithRow()
        {
            var list = container.Row;
            UIRect ret = null;
            for (var i = 0; i < list.Count; i++)
            {
                var edge = list[i];
                if (edge.UIRect is ClickUIRect)
                {
                    ret = edge.UIRect;
                    if (IsPrioritySelectUI(edge.UIRect.UIComponent.name))
                    {
                        return edge.UIRect;
                    }
                }
            }
            return ret;
        }

        protected UIRect FindInitUIRectWithColume()
        {
            var list = container.Column;
            UIRect ret = null;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var edge = list[i];
                if (edge.UIRect is ClickUIRect)
                {
                    ret = edge.UIRect;
                    if (IsPrioritySelectUI(edge.UIRect.UIComponent.name))
                    {
                        return edge.UIRect;
                    }
                }
            }
            return ret;
        }
     
        protected bool IsContainUIRect(UIContainer uicontainer, UIRect rect)
        {
           return uicontainer.Column.Contains(rect.DownEdge);
            for (var i = uicontainer.Row.Count - 1; i >= 0; i--)
            {
                if (uicontainer.Row[i].UIRect.UIComponent.GetInstanceID() == rect.UIComponent.GetInstanceID())
                    return true;
            }
            return false;
        }
        //直接点ui
        protected void SendUGUIClickEvent(UIRect click)
        {
            try
            {
                TrySendUGUIClickEvent(click);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        protected void TrySendUGUIClickEvent(UIRect click)
        {
            var clickui = click.UIComponent as IPointerClickHandler;
            if (clickui != null)
            {
                var e = new PointerEventData(EventSystem.current){ button = PointerEventData.InputButton.Left };
                clickui.OnPointerClick(e);
            }
        }
        //模拟点击
        protected void ClickEvent(UIRect click)
        {
            try
            {
                ClickTarget(click.Position,click.Layer);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
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

        protected bool IsCloseButton(UIRect ui)
        {
            string name = ui.UIComponent.gameObject.name;
            foreach (var closeUIName in CloseUINames)
                if (name == closeUIName) 
                    return true;
            return false;
        }
        public virtual void PointerMoveToUIRect(UIRect target,Direction direction)
        {
            if (target == null && PointerAttachUI == null)
            {
                Pointer.hide();
                return;
            }
            
            if(target != null) 
                PointerAttachUI = target;
            else if(PointerAttachUI == null)
                return;
            Pointer.show();
            Pointer.transform.position = PointerAttachUI.Position;
        }
       
        protected virtual UIRect GetUpUIRect(UIRect target,List<Edge> col)
        {
            if (target == null || ! IsContainUIRect(container,target)) 
                return null;
           
            UIRect box = FindBackUIRect(target.UpEdge.Index, col);
            return box;
        }
        
        protected virtual UIRect GetDownUIRect(UIRect target,List<Edge> column)
        {
            if (target == null || ! IsContainUIRect(container,target)) return null;
           
            UIRect box = FindForwardUIRect(target.DownEdge.Index, column);
            return box;
        }
        
        protected virtual UIRect GetLeftUIRect(UIRect target,List<Edge> Row)
        {
            if (target == null || ! IsContainUIRect(container,target))
                return null;
            try
            {
                UIRect box = FindForwardUIRect(target.LeftEdge.Index, Row);
                return box;
            }
            catch (Exception e)
            {
               Debug.LogError(target);
               LogContainer(true);
               throw;
            }

        }

        protected virtual UIRect GetRightUIRect(UIRect target, List<Edge> Row) {
            if (target == null || !IsContainUIRect(container, target))
                return null;
            UIRect box = FindBackUIRect(target.RightEdge.Index, Row);
            return box;
        }
        // up and right back find
        protected UIRect FindBackUIRect(int index,List<Edge> list)
        {
            float min = float.MaxValue;
            UIRect rect = null;
            for (int i = index+1; i < list.Count; i++)
            {
                if (list[i] is PreviousEdge && list[i].UIRect is ClickUIRect)
                {
                    
                    float distance = Vector2.Distance(list[index].UIRect.Position, list[i].UIRect.Position);
                    
                    if (min > distance)
                    {
                        min = distance;
                        rect= list[i].UIRect;
                    }
                }
            }
            return rect;
        }
        protected UIRect FindForwardUIRect(int index, List<Edge> list) {
            try {
                return TryFindForwardUIRect(index, list);
            }
            catch (Exception e) {
                Debug.LogError(e);
                Debug.LogError(index);
                Debug.LogError(list.Count);
                throw e;
            }
        }
        //left and down forword find
        protected UIRect TryFindForwardUIRect(int index, List<Edge> list) {
            float min = float.MaxValue;
            UIRect rect = null;
            for (int i = index - 1; i >= 0; i--) {
                if (list[i] is NextEdge && list[i].UIRect is ClickUIRect) {

                    float distance = Vector2.Distance(list[index].UIRect.Position, list[i].UIRect.Position);
                    if (min > distance) {
                        min = distance;
                        rect = list[i].UIRect;
                    }
                }
            }
            return rect;
        }
    }
}
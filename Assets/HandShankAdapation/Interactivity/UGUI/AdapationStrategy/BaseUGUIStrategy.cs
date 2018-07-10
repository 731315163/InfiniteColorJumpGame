using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HandShankAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.InputAdapation;
using Assets.HandShankAdapation.InputAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.UGUI;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy
{
    public abstract class BaseUGUIStrategy:KeyPressStrategy
    {
        public string[] CloseUINames = new string[0];
        public string[] PrioritySelectUINames = new string[0];
        protected UIContainer container;
        protected UIRect CanvasRoot;

        public float Delay = 0.25f;
        // Use this for initialization
      
        protected StrategyState strategy;
        public StrategyState Strategy
        {
            get { return strategy; }
            private set { strategy = value; }
        }
        public override StrategyState StrategyInit()
        {
            InitUINames();
            var state = new StrategyState(this);
            Strategy = state;
            return state;
        }
      
        protected void InitUINames()
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
                    uinames[i] = array[i];
                else
                    uinames[i] = array[ i - array.Length]+"(Clone)";
            }
            return uinames;
        }


        protected bool IsPrioritySelectUI(string selectname)
        {
            foreach (var prioname in PrioritySelectUINames)
                if (prioname == selectname)
                    return true;
            return false;
        }

       
        
        protected void LogContainer(bool isrow = false)
        {
            List<Edge> lists = isrow ? container.Row : container.Column;
            
            foreach (var edge in lists.OfType<PreviousEdge>())
                Debug.Log(edge.UIRect.UIComponent.name,edge.UIRect.UIComponent.gameObject);
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


        protected UIRect FindInitUIRect(IList<Edge> edges)
        {
            UIRect ret = null;
            for (var i = edges.Count - 1; i >= 0; i--)
            {
                var edge = edges[i];
                if (edge.UIRect is ClickUIRect)
                {
                    ret = edge.UIRect;
                    if (IsPrioritySelectUI(edge.UIRect.UIComponent.name))
                        return edge.UIRect;
                }
            }
            return ret;
        }
        protected bool IsContainUIRect(UIContainer uicontainer, UIRect rect)
        {
           return uicontainer.Column.Contains(rect.DownEdge);
        }
        //直接点ui
        protected void SendUGUIClickEvent(UIRect click)
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



        protected bool IsCloseButton(UIRect ui)
        {
            string name = ui.UIComponent.gameObject.name;
            foreach (var closeUIName in CloseUINames)
                if (name == closeUIName) 
                    return true;
            return false;
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
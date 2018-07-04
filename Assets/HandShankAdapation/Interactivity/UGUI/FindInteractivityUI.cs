using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HandShankAdapation.UGUI;
using Assets.HandShankAdapation.UGUI.UI;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.HandShankAdapation.Interactivity.UGUI
{
    public class FindInteractivityUI  : UIRectManager
    {
        public CanvasUIRect Canvas;
        public IFindUIRect[] Finders =  { new FindCanvasUI(),  new FindMaskUI(), new FindClickUI(), new FindImageUI()};
        protected Dictionary<int,UIRect> UIRectPool = new Dictionary<int, UIRect>();
        public override UIContainer FindActivityUIRectsOnMask()
        {
            UIContainer container = new UIContainer();
            var uirects = FindActivityUIRectsWithCanvas();
            //按层级关系排序
            uirects.Sort((a, b) =>
            {
                int sort = a.SortOrder.CompareTo(b.SortOrder);
                return sort != 0 ? sort : a.DeepOrder.CompareTo(b.DeepOrder);
            });
            for (var i = 0; i < uirects.Count; i++)
            {
                if (!IsShowWithMaskFather(uirects[i].Parent, uirects[i]))
                    continue;
                if (IsCanShow(uirects, i) && uirects[i] is ClickUIRect)
                    container.Insert(uirects[i]);
            }
            container.Sort();
            return container;
        }
      
        protected bool IsShowWithMaskFather(UIRect f, UIRect t)
        {
            if (f == null || t == null) return true;
            if (f is MaskUIRect)
            {
                if (f.ContainPercent(t) > 0.705f)
                    return true;
                return false;
            }
            return IsShowWithMaskFather(f.Parent, t);
        }

        protected bool IsCanShow(IList<UIRect> uirects, int i)
        {
            for (int j = i + 1; j < uirects.Count; j++)
            {
                var graphic = uirects[j].UIComponent as MaskableGraphic;
                if (graphic == null)
                    continue;
                float containper = uirects[j].ContainPercent(uirects[i]);

                if (graphic.raycastTarget &&
                    !(uirects[j].UIComponent is Text) &&
                    containper > 0.905f && containper <= 1f)
                    return false;
            }
            return true;
        }
        public override UIContainer FindActivityUIRects()
        {
            UIContainer container = new UIContainer();
            var uirects = FindActivityUIRectsWithCanvas();
            foreach (var rect in uirects)
                container.Insert(rect);
            container.Sort();
            return container;
        }
        

        protected List<UIRect> FindActivityUIRectsWithCanvas()
        {
            if(Canvas == null || ! Canvas.IsActivite) Canvas = GetRoot() as CanvasUIRect;
            var uirects = new List<UIRect>();
            int deeporder = 0;
            FindActivityUIRects(uirects, Canvas.UIComponent.transform, null, Canvas.SortOrder, ref deeporder);
            return uirects;
        }

       
        public override void SetRoot(Behaviour root)
        {
            Canvas c = root as Canvas;
            if(c == null)
                return;
            UIRect rootui;
            if (UIRectPool.TryGetValue(root.transform.GetInstanceID(), out rootui))
                this.Canvas = rootui as CanvasUIRect;
            else 
                this.Canvas = new CanvasUIRect(root);
        }

       
        public string[] UIRootNames;
        public override UIRect GetRoot()
        {
            var canvases = Object.FindObjectsOfType<Canvas>();
           
            var root = canvases.FirstOrDefault();
            for (int i = 0; i <UIRootNames.Length; i++)
            {
                foreach (var canvas in canvases)
                {
                    if( ! canvas.enabled)
                        continue;
                    if (UIRootNames[i] == canvas.name)
                    {
                        root = canvas;
                        return GetRoot(root);
                    }
                }
            }
            return root == null ? null : GetRoot(root);
        }

        protected CanvasUIRect GetRoot(Canvas root)
        {
            var find = new FindCanvasUI();
            foreach (var component in root.GetComponents<Behaviour>())
            {
                var container = find.Find(component);
                if (container != null)
                {
                    return container as CanvasUIRect;
                }
            }
            return null;
        }
       
        protected void FindActivityUIRects(List<UIRect> container,Transform transf, UIRect father,int sortorder,ref int deeporder)
        {
            foreach (Transform o in transf)
            {
                if (o.gameObject.activeInHierarchy)
                {
                    UIRect rect = FindUIRect(o);

                    if (rect == null)
                        FindActivityUIRects(container, o, father, sortorder, ref deeporder);
                    else
                    {
                        ++deeporder;
                        rect.DeepOrder = deeporder;
                        rect.Parent = father;
                        container.Add(rect);
                        var canvas = rect as CanvasUIRect;
                        if (canvas != null)
                        {
                            canvas.SortOrder = canvas.UIComponent.GetComponent<Canvas>().sortingOrder;
                            FindActivityUIRects(container, o, father, rect.SortOrder, ref deeporder);
                        }
                        else 
                        {
                            rect.SortOrder = sortorder;
                            FindActivityUIRects(container, o, rect, sortorder, ref deeporder);
                        }
                    }
                }
            }
        }

        public UIRect FindUIRect(Transform com)
        {
            
            UIRect uirect = null;
            if (UIRectPool.TryGetValue(com.GetInstanceID(), out uirect) && uirect.IsActivite)
                return uirect;
            
            var comps = com.GetComponents<Behaviour>();
            
            int index = Int32.MaxValue;
            foreach (var comp in comps)
            {
                if( ! comp.enabled)
                    continue;
                
                for (int i = 0; i < Finders.Length; ++i)
                {
                    var find = Finders[i].Find(comp);
                    if (find != null && i < index)
                    {
                        uirect = find;
                        index = i;
                        break;
                    }
                }
            }
            if(uirect != null)
                UIRectPool.Add(uirect.InstanceID,uirect);
                
            return uirect;
        }
    }
}
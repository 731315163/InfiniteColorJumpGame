using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.HandShankAdapation.UIBounds
{
    public abstract class UIRect
    {

        public Behaviour UIComponent;
        public int SortOrder;
        public UIRect Parent;
        //public UIContainer ParentContainer;
        public  abstract Vector3 Position { get;}
        public abstract int Layer { get; }
        public abstract float Left { get; }
        public  abstract float Right { get; }
        public abstract float Down { get; }
        public abstract float Up { get; }
        /// <summary>
        /// gameobject树状层级关系
        /// </summary>
        public int DeepOrder; 
        public LeftEdge LeftEdge;
        public RightEdge RightEdge;
        public UpEdge UpEdge;
        public DownEdge DownEdge;
        private List<UIRect> children;

        public List<UIRect> Children
        {
            get { return children ?? (children = new List<UIRect>()); }
        }

        public int InstanceID
        {
            get { return UIComponent.transform.GetInstanceID(); }
        }
        /// <summary>
        /// x is left, y is down,z is right ,w is up
        /// </summary>
        public Vector4 ViewPosition;
      
        protected UIRect(Behaviour go)
        {
            this.UIComponent = go;
        }
        public bool IsDestroy
        {
            get
            {
                if (UIComponent == null) 
                    return true;
                return false;
            }
        }
        public bool IsActivite
        {
            get
            {
                if ( ! IsDestroy)
                    return UIComponent.gameObject.activeInHierarchy && UIComponent.enabled;
                return false;
            }
        }

        public bool IsContain(UIRect box)
        {
            if( Left <= box.Left && Right >= box.Right  && Up >= box.Up && Down <= box.Down)return true;
            return false;
        }

        public bool IsContain(Vector2 point)
        {
            if (point.x < Left || point.x > Right || point.y > Up || point.y < Down) return false;
            return true;
        }
        public float ContainPercent(UIRect box)
        {
            if (Left > box.Right || Right < box.Left || Up < box.Down || Down > box.Up) return 0f;
            float left,right,up,down;
            left = box.Left < this.Left ? this.Left : box.Left;

            right = box.Right > this.Right ? this.Right : box.Right;

            up = box.Up > this.Up ? this.Up : box.Up;

            down = box.Down < this.Down ? this.Down : box.Down;

            float width, height,area;
            width =Mathf.Abs(right - left);
            height =Mathf.Abs( up - down);
            area = width * height;
            float actualarea = Mathf.Abs(box.Right - box.Left) *Mathf.Abs(box.Up - box.Down);
            return area /actualarea;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" name : ");
            s.Append(UIComponent.gameObject.name);
            s.Append(" L : ");
            s.Append(Left);
            s.Append(" R : ");
            s.Append(Right);
            s.Append(" U : ");
            s.Append(Up);
            s.Append(" D :");
            s.Append(Down);
            return s.ToString();
        }
    }
}

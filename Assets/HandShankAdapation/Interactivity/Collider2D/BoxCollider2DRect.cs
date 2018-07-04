using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;

namespace Assets.HandShankAdapation.Collider2D
{
       public class BoxCollider2DRect:UIRect
    {
        protected Transform transform;
        public BoxCollider2D Collider;

        public override int Layer
        {
            get { return transform.gameObject.layer; }
        }

        public BoxCollider2DRect(MonoBehaviour go) : base(go)
        {
            this.transform = go.transform;
            LeftEdge = new LeftEdge(this);
            RightEdge = new RightEdge(this);
            UpEdge = new UpEdge(this);
            DownEdge = new DownEdge(this);
          
            
        }

        public override Vector3 Position
        {
            get { return transform.position; }
        }

        public float XScale()
        {
            float scale = 1.0f;
            Transform t = Collider.transform;
            while (t != null)
            {
                scale *= t.localScale.x;
                t = t.parent;
            }
            return scale;
        }

        public float YScale()
        {
            float scale = 1.0f;
            Transform t = Collider.transform;
            while (t != null)
            {
                scale *= t.localScale.y;
                t = t.parent;
            }
            return scale;
        }
        /// <summary>
        /// 不考虑父级缩放和旋转
        /// </summary>
        /// <returns></returns>
        public override float Left
        {
            get
            {
                return (transform.position.x + Collider.offset.x) - (Collider.size.x/ 2 * XScale()) ; 
            }
          
        }
        

      
        public  override float Right
        {
            get
            {
                return (Collider.transform.position.x + Collider.offset.x + Collider.size.x / 2 * XScale()) ; 
            }
            
        }


        public override float Up
        {
            get
            {
                return (Collider.transform.position.y + Collider.offset.y + Collider.size.y / 2 * YScale()) ; 
            }
           
        }

        public override float Down
        {
            get
            {
                return (Collider.transform.position.y + Collider.offset.y - Collider.size.y / 2 * YScale()) ; 
            }
            
        }
    }

  
}

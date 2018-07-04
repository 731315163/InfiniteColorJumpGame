using System;
using System.Collections.Generic;
using Assets.HandShankAdapation.UIBounds;
using UnityEngine;

namespace Assets.HandShankAdapation.UGUI
{
    public class UGUIRect:UIRect
    {
        protected RectTransform transform;
        protected Vector3[] Corners = new Vector3[4];
        //left up right down
        protected int[] beginposition = {1, 2, 3, 0};
        protected Queue<int> roulette  = new Queue<int>(new[]{0,1,2,3});
        private int layer;

        public override int Layer
        {
            get { return transform.gameObject.layer; }
        }

        public UGUIRect(Behaviour go) : base(go)
        {
            this.transform = go.GetComponent<RectTransform>();
            LeftEdge = new LeftEdge(this);
            RightEdge = new RightEdge(this);
            UpEdge = new UpEdge(this);
            DownEdge = new DownEdge(this);
            transform.GetWorldCorners(Corners);
        }

        public override Vector3 Position
        {
            get
            {
                if (transform.pivot.x - 0.5f < 0.01 && transform.pivot.y - 0.5 < 0.01f) return transform.position;
                return new Vector3(Left+(Right-Left)/2f,Down+(Up-Down)/2f);
            }
        }

        public override float Left
        {
            get { return GetRotateCorner(0).x; }
        }
        public override float Down
        {
            get { return GetRotateCorner(3).y; }
        }
        public override float Right
        {
            get { return GetRotateCorner(2).x; }
        }

        public override float Up
        {
            get { return GetRotateCorner(1).y; }
        }

        protected Vector3 GetRotateCorner(int index)
        {
            transform.GetWorldCorners(Corners);
            int rotatetimes = (int)(transform.eulerAngles.z / 90f);
            return  Corners[GetRotateRoulette(beginposition[index], rotatetimes)];
        }

        protected int GetRotateRoulette(int headindex, int roratetimes)
        {
            SetRoulette(headindex);
            int index = roulette.Peek();
            while (roratetimes > 0)
            {
                --roratetimes;
                roulette.Enqueue(roulette.Dequeue());
                index = roulette.Peek();
            }
            return index;
        }
        protected  void SetRoulette(int i)
        {
            if (i < 0 || i > 3) throw new IndexOutOfRangeException();
            while (roulette.Peek() != i)
            {
                roulette.Enqueue(roulette.Dequeue());
            }
        }
    }
}

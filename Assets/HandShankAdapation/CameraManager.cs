using System.Collections.Generic;
using Assets.HandShankAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.UIBounds;
using GDGeek;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.HandShankAdapation
{
    public class CameraManager:Singleton<CameraManager>
    {
        protected List<Camera> cameraes = new List<Camera>();
        public Vector2 Correction;
        void Awake()
        {

            var transforms = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var t in transforms)
            {
                cameraes.AddRange(FindCamera(t.transform));
            }
        }

        protected IEnumerable<Camera> FindCamera(Transform t)
        {
            var camera = t.GetComponent<Camera>();
            if (camera != null) yield return camera;
            foreach (Transform o in t)
            {
                foreach (var c in FindCamera(o))
                {
                    yield return c;
                }
            }
        }

        public Camera GetCameraWithLayer(int layer)
        {
            foreach (var camerae in cameraes)
            {
                if ((camerae.cullingMask & (1 << layer)) > 0)
                {
                    return camerae;
                }
            }
            return null;
        }

        public Vector4 GetRectViewPosition(UIRect box)
        {
            Camera c = GetCameraWithLayer(box.UIComponent.gameObject.layer);
            var leftdown = c.WorldToViewportPoint(new Vector3(box.Left, box.Down, 0));
            var rightup = c.WorldToViewportPoint(new Vector3(box.Right, box.Up, 0));
            return new Vector4(leftdown.x,leftdown.y,rightup.x,rightup.y);
        }
        
        public Vector2 UIRectPositionToScreenPosition(UIRect box)
        {
            Camera c = GetCameraWithLayer(box.UIComponent.gameObject.layer);
            return c.WorldToScreenPoint(box.Position);
        }
        /// <summary>
        /// 转换为目标设备的屏幕实际坐标系，不是unity坐标系
        /// </summary>
        /// <param name="position">物体世界坐标</param>
        /// <param name="layer">物体所在的层，gameobject的layer属性,用于获取渲染gameobject的相机</param>
        /// <returns></returns>
        public Vector2 WorldPositionToScreenPosition(Vector3 position,int layer)
        {
            Camera c = GetCameraWithLayer(layer);
            return WorldPositionToScreenPosition(position, c);
        }

        public Vector2 WorldPositionToScreenPositionWithScreenOverlay(Vector3 position)
        {
            Vector2 v = RectTransformUtility.WorldToScreenPoint(null, position);
            SetCorrection();
            return new Vector2(v.x + Correction.x, Correction.y - v.y);
        }

        public Vector2 WorldPositionToScreenPosition(Vector3 position, Camera c)
        {
          
            Vector2 v = c.WorldToScreenPoint(position);
            SetCorrection();
            return new Vector2(v.x+Correction.x,Correction.y - v.y);
        }
        /// <summary>
        /// unity坐标与屏幕坐标的换算
        /// </summary>
        public void SetCorrection()
        {
#if UNITY_EDITOR
            Point p;
            PcImitateInput.GetCursorPos(out p);
            Correction.x = p.X - Input.mousePosition.x;
            Correction.y = p.Y + Input.mousePosition.y;
#else
             Correction.x = 0;
            Correction.y = Screen.height;
#endif

        }

    }
}

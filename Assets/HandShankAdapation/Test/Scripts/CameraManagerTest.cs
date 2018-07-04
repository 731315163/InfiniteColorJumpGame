using System;
using Assets.HandShankAdapation.ImitateAndroidInput;
using Assets.HandShankAdapation.InputAdapation.ImitateAndroidInput;
using UnityEngine;

namespace Assets.HandShankAdapation.Test.Scripts
{
    public class CameraManagerTest : MonoBehaviour
    {

       

      
        // Use this for initialization
        void Start ()
        {
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WorldPositionToScreenPositionTest();
            }
        }
        void Invoke()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public void WorldPositionToScreenPositionTest()
        {
            Vector2 position = CameraManager.Instance.WorldPositionToScreenPosition(transform.position, gameObject.layer);
            ImitateInputManager.Instance.OnPressEvent(position);
            ImitateInputManager.Instance.OnUpEvent(position);
            
        }

        void OnMouseDown()
        {
            Debug.Log("ClickMe");
        }
	
    }
}

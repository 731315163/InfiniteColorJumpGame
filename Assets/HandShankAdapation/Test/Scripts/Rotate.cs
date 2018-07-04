#if UNITY_EDITOR
using Assets.HandShankAdapation.ImitateAndroidInput;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public GameObject point;

    public Point p;
	// Use this for initialization
	void Start ()
	{
	    point = Instantiate(point, this.transform);
	}
	
	// Update is called once per frame
	void Update () {
	   
		transform.Rotate(Vector3.forward,Time.deltaTime * -10,Space.World);
	    float rotate =Mathf.Deg2Rad*( 360f - transform.eulerAngles.z);
	    float x = Mathf.Sin(rotate);
	    float y = Mathf.Cos(rotate);
	    Debug.Log("unity position : X " + Input.mousePosition.x + ",Y : "+ Input.mousePosition.y );
	    if (Input.GetMouseButton(0))
	    {
	        PcImitateInput.GetCursorPos(out p);
            Debug.Log("Screent X : " + Screen.width + "Screen Y" + Screen.height);
            Debug.Log(p);
            Debug.Log("unity position : X " + Input.mousePosition.x + ",Y : "+ Input.mousePosition.y );
	    }
	}
    void OnGUI(){
        var e=Event.current;
        if(e.isKey){
            Debug.Log("key:"+e.keyCode);
        }
    }
}
#endif
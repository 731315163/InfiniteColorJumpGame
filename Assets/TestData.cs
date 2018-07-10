using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    PlayerPrefs.SetFloat("Money", 1000);
        PlayerPrefs.Save();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

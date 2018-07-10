using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
	}

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        DestroyObject(this);
        Debug.Log("Scene1" + arg0.name);
        Debug.Log("Scene2" + arg1.name);
    }

}

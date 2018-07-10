using UnityEngine;
using UnityEngine.UI;

public class ApplicationQuit : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var but = this.GetComponent<Button>();
        if(but) but.onClick.AddListener(Quit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Quit()
    {
        Application.Quit();
    }
}
